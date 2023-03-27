using Microsoft.AspNetCore.Mvc;
using SillyString.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SillyString.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      ViewBag.Engineers = _db.Engineers
                      .Include(book => book.EngineerMachine)
                      .ThenInclude(join => join.Engineer)
                      .ToList();
      ViewBag.Machines = _db.Machines
                      .Include(author => author.EngineerMachine)
                      .ThenInclude(join => join.Machine)
                      .ToList();
      return View();
    }    
  }
}