using EFUApi.Filters.ActionFilters;
using EFUApi.Filters.ExceptionFilters;
using EFUApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

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
    [Course_HandleUpdateExceptionsFilter]
    public IActionResult UpdateCourse(int id, Course course)
    {
        CourseRepository.UpdateCourse(course);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public string DeleteCourse(int id)
    {
        return $"Deleting course: {id}";
    }
}
