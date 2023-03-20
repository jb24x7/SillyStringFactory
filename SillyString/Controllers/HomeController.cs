using Microsoft.AspNetCore.Mvc;

namespace SillyString.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }    
  }
}