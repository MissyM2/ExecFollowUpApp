﻿
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
      // adding try/catch blocks to use our new WebApiExecutor exception handler
      try
      {
        var response = await webApiExecutor.InvokePost("courses", course);
        if (response != null)
        {
          return RedirectToAction(nameof(Index));
        }
      }
      catch(WebApiException ex)
      {
       HandleWebApiException(ex); 
      }
      
    }
    return View(course);
  }

// you do not need an HTTP verb get because GET is the default
  public async Task<IActionResult> UpdateCourse(int courseId)
  {
    try
    {
      var course = await webApiExecutor.InvokeGet<Course>($"courses/{courseId}");
      if (course != null)
      {
        return View(course);
      }

    }
    catch(WebApiException ex)
    {
      HandleWebApiException(ex);
      return View();
    }
    

    return NotFound();
  }

  // This is NOT WebApi, this is Http Action Method so, although this is an update method
  // we want to POST to this Action Method..  We keep the HttpPost decorator for the method.

  [HttpPost]
  public async Task<IActionResult> UpdateCourse(Course course)
  {

    // in webApi, model can be validated BEFORE calling the method, but
    // this isn't available in webApp.  We have to validate within the method
    if (ModelState.IsValid)
    {
      try
      {
          // string interpolation is used here.  The POST endpoint in the WebApi is expecting the id AND the course body
          await webApiExecutor.InvokePut($"courses/{course.CourseId}", course);
          return RedirectToAction(nameof(Index));

      }
      catch(WebApiException ex)
      {
        HandleWebApiException(ex);
      }

    
    }
    return View(course);
  }

  public async Task<IActionResult> DeleteCourse(int courseId)
  {
    try
    {
      await webApiExecutor.InvokeDelete($"courses/{courseId}");
      return RedirectToAction(nameof(Index),
          await webApiExecutor.InvokeGet<List<Course>>("courses"));

    }
    catch(WebApiException ex)
    {
      HandleWebApiException(ex);
      // this is different.  We are going to lose the ModelState when we delete the course.
      // In order to keep the ModelState and display the message, do this.
      return View("Index");

    }
    
  }

// LATER, create a Base Controller and put this method in it
  private void HandleWebApiException(WebApiException ex)
  {
    if (ex.ErrorResponse != null &&
            ex.ErrorResponse.Errors != null &&
            ex.ErrorResponse.Errors.Count > 0)
            {
              foreach(var error in ex.ErrorResponse.Errors)
              {
                ModelState.AddModelError(error.Key, string.Join("; ", error.Value));
              }
            }
  }


}
