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
    private readonly SillyStringContext _db;
        public HomeController(SillyStringContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      ViewBag.Engineers = _db.Engineers
                      .Include(book => book.JoinEntities)
                      .ThenInclude(join => join.Engineer)
                      .ToList();
      ViewBag.Machines = _db.Machines
                      .Include(author => author.JoinEntities)
                      .ThenInclude(join => join.Machine)
                      .ToList();
      return View();
    }    
  }
}