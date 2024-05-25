using System.Security.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EFUApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    [HttpGet]
    public string GetCourses()
    {
        return "Reading all the courses";
    }

    [HttpGet("{id}")]
    public string GetCourseById(int id)
    {
        return $"Reading course: {id}";
    }

    [HttpPost]
    public string CreateCourse([FromBody]Course course)
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
