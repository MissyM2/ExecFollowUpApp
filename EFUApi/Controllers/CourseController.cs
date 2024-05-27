using System.Security.Permissions;
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
    public IActionResult CreateCourse([FromBody] Course course)
    {
        if (course == null) return BadRequest();

        var existingCourse = CourseRepository.GetCourseByProperties(course.Code, course.CourseNum, course.Name, course.Description);
        if (existingCourse != null) return BadRequest();

        CourseRepository.AddCourse(course);

        return CreatedAtAction(nameof(GetCourseById),
            new {id = course.CourseId},
            course);
    }

    [HttpPut("{id}")]
    public string UpdateCourse(int id)
    {
        return $"Updating Course: {id}";
    }

    [HttpDelete("{id}")]
    public string DeleteCourse(int id)
    {
        return $"Deleting course: {id}";
    }
}
