using EFUApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EFUApi.Filters.ActionFilters;

public class Course_ValidateCourseIdFilterAttribute : ActionFilterAttribute
{
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
          else if (!CourseRepository.CourseExists(courseId.Value))
          {
            context.ModelState.AddModelError("CourseId", "Course doesn't exist.");
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
              Status = StatusCodes.Status404NotFound
            };

            
            context.Result = new NotFoundObjectResult(problemDetails);
          }
        }
    }

}
