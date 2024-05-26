using System.Security.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EFUApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private List<Course> courses = new List<Course>()
    {
        new Course {CourseId = 1, Code = "ENG101", Name = "English", Description = "Poetry from 1900"},
        new Course {CourseId = 2, Code = "MATH101", Name = "Math", Description = "Calculus"},
        new Course {CourseId = 3, Code = "PHIL101", Name = "Philosophy", Description = "Logic"},
        new Course {CourseId = 4, Code = "PSYCH101", Name = "Psychology", Description = "Psychology of Women"}
    };

    [HttpGet]
    public string GetCourses()
    {
        return "Reading all the courses";
    }

    [HttpGet("{id}")]
    public IActionResult GetCourseById(int id)
    {
        if (id <= 0)
        return BadRequest();
        
        var course = courses.FirstOrDefault(x => x.CourseId == id);
        if (course == null)
            return NotFound();
        return Ok(course);
    }

    [HttpPost]
    public string CreateCourse([FromBody] Course course)
    {
        return $"Creating a course";
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
