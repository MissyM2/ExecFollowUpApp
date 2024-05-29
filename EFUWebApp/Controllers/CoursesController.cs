
using System.Runtime.CompilerServices;
using EFUWebApp.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EFUWebApp.Controllers;

public class CoursesController : Controller
{
  private readonly IWebApiExecuter webApiExecuter;
  public CoursesController(IWebApiExecuter webApiExecuter)
  {
    this.webApiExecuter = webApiExecuter;
  }
  public async Task<IActionResult> Index()
  {
    return View(await webApiExecuter.InvokeGet<List<Course>>("courses"));
  }

  public IActionResult CreateCourse()
  {
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> CreateCourse(Course course)
  {
    return View(course);
  }

}
