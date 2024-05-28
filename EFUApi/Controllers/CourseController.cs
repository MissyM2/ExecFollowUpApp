using EFUApi.Data;
using EFUApi.Filters.ActionFilters;
using EFUApi.Filters.ExceptionFilters;
using EFUApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EFUApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ApplicationDbContext db;
    public CoursesController(ApplicationDbContext db)
    {
        this.db = db;
    }
    

    [HttpGet]
    public IActionResult GetCourses()
    {
        return Ok(db.Courses.ToList());
    }

    [HttpGet("{id}")]
    // when using ApplicationDbContext, you have to use a filter in this way
    [TypeFilter(typeof(Course_ValidateCourseIdFilterAttribute))]
    public IActionResult GetCourseById(int id)
    {
        // you have gotten the course, including the course id through the ValidateCourseIdFilter
        // so, on line 51 of the filter, you have added the course to the HttpContext
        // you can access the context to get the course rather than going to the db
        // twice; you have access to the HttpContext through your Action Method
        
        
        return Ok(HttpContext.Items["course"]);
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
    [TypeFilter(typeof(Course_ValidateCourseIdFilterAttribute))]
    [Course_ValidateUpdateCourseFilter]
    [Course_HandleUpdateExceptionsFilter]
    public IActionResult UpdateCourse(int id, Course course)
    {
        CourseRepository.UpdateCourse(course);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [TypeFilter(typeof(Course_ValidateCourseIdFilterAttribute))]
    public IActionResult DeleteCourse(int id)
    {
        var course = CourseRepository.GetCourseById(id);

        // right now, this is a HARD DELETE (removed from in-memory data store); however, usually, 
        // a programmer wants to mark for deletion.. a SOFT DELETE
        CourseRepository.DeleteCourse(id);

        return Ok(course);
    }
}
