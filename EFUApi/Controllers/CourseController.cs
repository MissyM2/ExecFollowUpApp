using EFUApi.Data;
using EFUApi.Filters.ActionFilters;
using EFUApi.Filters.AuthFilters;
using EFUApi.Filters.ExceptionFilters;
using EFUApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EFUApi.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[JwtTokenAuthFilter]
public class CoursesController : ControllerBase
{
    private readonly ApplicationDbContext db;
    public CoursesController(ApplicationDbContext db)
    {
        this.db = db;
    }
    

    [HttpGet]

    // doing this manually.  Should implement Asp .Net Core Identity Framework
    [RequiredClaim("read", "true")]
    public IActionResult GetCourses()
    {
        return Ok(db.Courses.ToList());
    }

    [HttpGet("{id}")]
    // when using ApplicationDbContext, you have to use a filter in this way
    [TypeFilter(typeof(Course_ValidateCourseIdFilterAttribute))]
     // doing this manually.  Should implement Asp .Net Core Identity Framework
    [RequiredClaim("read", "true")]
    public IActionResult GetCourseById(int id)
    {
        // you have gotten the course, including the course id through the ValidateCourseIdFilter
        // so, on line 51 of the filter, you have added the course to the HttpContext
        // you can access the context to get the course rather than going to the db
        // twice; you have access to the HttpContext through your Action Method
        
        
        return Ok(HttpContext.Items["course"]);
    }

    [HttpPost]
    [TypeFilter(typeof(Course_ValidateCreateCourseFilterAttribute))]
     // doing this manually.  Should implement Asp .Net Core Identity Framework
    [RequiredClaim("write", "true")]
    public IActionResult CreateCourse([FromBody] Course course)
    {
        this.db.Courses.Add(course);
        this.db.SaveChanges();

        return CreatedAtAction(nameof(GetCourseById),
            new {id = course.CourseId},
            course);
    }

    [HttpPut("{id}")]
    [TypeFilter(typeof(Course_ValidateCourseIdFilterAttribute))]
    [Course_ValidateUpdateCourseFilter]
    [TypeFilter(typeof(Course_HandleUpdateExceptionsFilterAttribute))]
     // doing this manually.  Should implement Asp .Net Core Identity Framework
    [RequiredClaim("write", "true")]
    public IActionResult UpdateCourse(int id, Course course)
    {
        var courseToUpdate = HttpContext.Items["course"] as Course;
        courseToUpdate.Code = course.Code;
        courseToUpdate.CourseNum = course.CourseNum;
        courseToUpdate.Name = course.Name;
        courseToUpdate.Description = course.Description;

        db.SaveChanges();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [TypeFilter(typeof(Course_ValidateCourseIdFilterAttribute))]
     // doing this manually.  Should implement Asp .Net Core Identity Framework
    [RequiredClaim("delete", "true")]    
    public IActionResult DeleteCourse(int id)
    {
        // need to retrieve the shirt
       var courseToDelete = HttpContext.Items["course"] as Course;

       db.Courses.Remove(courseToDelete);
       db.SaveChanges();

        return Ok(courseToDelete);
    }
}
