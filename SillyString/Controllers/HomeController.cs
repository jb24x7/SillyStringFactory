using Microsoft.AspNetCore.Mvc;

namespace ElectronicsVendor.Controllers
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