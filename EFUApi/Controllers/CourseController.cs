using System.Security.Permissions;
using EFUApi.Filters;
using EFUApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EFUApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    

    [HttpGet]
    public IActionResult GetCourses()
    {
        return Ok(CourseRepository.GetCourses());
    }

    [HttpGet("{id}")]
    [Course_ValidateCourseIdFilter]
    public IActionResult GetCourseById(int id)
    {
        
        return Ok(CourseRepository.GetCourseById(id));
    }

    [HttpPost]
    [Course_ValidateCreateCourseFilter]
    public IActionResult CreateCourse([FromBody] Course course)
    {
        CourseRepository.AddCourse(course);

        return CreatedAtAction(nameof(GetCourseById),
            new {id = course.CourseId},
            course);
    }

    [HttpPut("{id}")]
    [Course_ValidateCourseIdFilter]
    [Course_ValidateUpdateCourseFilter]
    public IActionResult UpdateCourse(int id, Course course)
    {
        // if course is deleted after the line above but before update, then use try/catch block
        try
        {
            CourseRepository.UpdateCourse(course);

        }
        catch
        {
            if (!CourseRepository.CourseExists(id))
                return NotFound();

            // if it fails for any other reason, throw generic 500 error
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public string DeleteCourse(int id)
    {
        return $"Deleting course: {id}";
    }
}
