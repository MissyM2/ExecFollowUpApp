using EFUApi.Data;
using EFUApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EFUApi.Filters.ActionFilters;

public class Course_ValidateCourseIdFilterAttribute : ActionFilterAttribute
{
  private readonly ApplicationDbContext db;

  public Course_ValidateCourseIdFilterAttribute(ApplicationDbContext db)
  {
    this.db = db;
  }
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var courseId = context.ActionArguments["id"] as int?;
        if (courseId.HasValue)
        {
          if (courseId.Value <= 0)
          {
            context.ModelState.AddModelError("CourseId", "CourseId is invalid.");
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
              Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(problemDetails);
          }
          else
          {
            var course = db.Courses.Find(courseId.Value);

            if (course == null)
            {
              // spit out this error
              context.ModelState.AddModelError("CourseId", "Course doesn't exist.");
              var problemDetails = new ValidationProblemDetails(context.ModelState)
              {
                Status = StatusCodes.Status404NotFound
              };
              context.Result = new NotFoundObjectResult(problemDetails);
            }
            else
            {
              // context is temporarily storing the course.  This way
              // if you are using GetCourseById, for example, it can get the id from 
              // the context rather than hitting the db twice
              context.HttpContext.Items["course"] = course;
            }
            
          }
        }
    }

}
