
using EFUWebApp.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EFUWebApp.Controllers;

public class CoursesController : Controller
{
  public IActionResult Index()
  {
    return View(CourseRepository.GetCourses());
  }

}
