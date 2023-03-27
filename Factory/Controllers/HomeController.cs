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
      Machine[] machines = _db.Machines.ToArray();
      Engineer[] engineers = _db.Engineers.ToArray();
      Dictionary<string, object[]> model = new Dictionary<string, object[]>();
      model.Add("machines", machines);
      model.Add("engineers", engineers);
      return View(model);
    }    
  }
}