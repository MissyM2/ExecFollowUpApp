using EFUApi.Data;
using EFUApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EFUApi.Filters.ActionFilters;
public class Course_ValidateCreateCourseFilterAttribute : ActionFilterAttribute
{
  private readonly ApplicationDbContext db;

  public Course_ValidateCreateCourseFilterAttribute(ApplicationDbContext db)
  {
    this.db = db;
    
  }
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var course = context.ActionArguments["course"] as Course;

        // check to see if course is null
        if (course == null)
        {
          context.ModelState.AddModelError("Course", "Course object is null.");
          var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
              Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(problemDetails);
        }

        // check to see if course already exists
        else
        {
          var existingCourse = db.Courses.FirstOrDefault(x => 
            !string.IsNullOrWhiteSpace(course.Code) &&
            !string.IsNullOrWhiteSpace(x.Code) &&
            x.Code.ToLower() == course.Code.ToLower());

          if (existingCourse != null)
          {
            context.ModelState.AddModelError("Course", "Course already exists.");
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
              Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(problemDetails);
          }
          
        }
   }

}
