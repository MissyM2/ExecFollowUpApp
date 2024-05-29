
using System.Runtime.CompilerServices;
using EFUWebApp.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EFUWebApp.Controllers;

public class CoursesController : Controller
{
  private readonly IWebApiExecutor webApiExecutor;
  public CoursesController(IWebApiExecutor webApiExecutor)
  {
    this.webApiExecutor = webApiExecutor;
  }
  public async Task<IActionResult> Index()
  {
    return View(await webApiExecutor.InvokeGet<List<Course>>("courses"));
  }

  public IActionResult CreateCourse()
  {
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> CreateCourse(Course course)
  {

    // in webApi, model can be validated BEFORE calling the method, but
    // this isn't available in webApp.  We have to validate within the method
    if (ModelState.IsValid)
    {
      var response = await webApiExecutor.InvokePost("courses", course);
      if (response != null)
      {
        return RedirectToAction(nameof(Index));
      }
    }
    return View(course);
  }

}
