using EFUApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EFUApi.Filters.ActionFilters;

public class Course_ValidateUpdateCourseFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var id = context.ActionArguments["id"] as int?;
        var course = context.ActionArguments["course"] as Course;

        if (id.HasValue && course != null && id != course.CourseId)
        {
          context.ModelState.AddModelError("Course", "CourseId is not the same as id.");
          var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
              Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(problemDetails);

        }
    }
}