using EFUApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EFUApi.Filters;
public class Course_ValidateCreateCourseFilterAttribute : ActionFilterAttribute
{
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
          var existingCourse = CourseRepository.GetCourseByProperties(course.Code, course.CourseNum, course.Name, course.Description);
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
