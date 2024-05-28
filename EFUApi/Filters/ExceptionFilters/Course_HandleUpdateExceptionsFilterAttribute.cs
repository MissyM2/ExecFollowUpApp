using EFUApi.Data;
using EFUApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EFUApi.Filters.ExceptionFilters;

public class Course_HandleUpdateExceptionsFilterAttribute: ExceptionFilterAttribute
{
  private readonly ApplicationDbContext db;
  public Course_HandleUpdateExceptionsFilterAttribute(ApplicationDbContext db)
  {
    this.db = db;
  }
    public override void OnException(ExceptionContext context)
    {
        base.OnException(context);

        // you need the id and can get it from RouteData, an object that includes id, however, it is a string
        var strCourseId = context.RouteData.Values["id"] as string;

        // use TryParse to convert to int
        if (int.TryParse(strCourseId, out int courseId))
        {
          if (db.Courses.FirstOrDefault(x => x.CourseId == courseId) == null)
          {
            context.ModelState.AddModelError("CourseId", "Shirt doesn't exist anymore.");
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
              Status = StatusCodes.Status404NotFound
            };
            context.Result = new NotFoundObjectResult(problemDetails);
          }
          
        }
    }

}
