using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EFUApi.Filters.V2;

public class Course_EnsureInstructorIsPresentFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var course = context.ActionArguments["course"] as Course;
        if (course != null && !course.ValidateInstructor())
        {
          context.ModelState.AddModelError("Course", "Instructor is required.");
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
              Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(problemDetails);

        }
    }
}
