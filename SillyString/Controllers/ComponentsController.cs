using Microsoft.AspNetCore.Mvc;
using ElectronicsVendor.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectronicsVendor.Controllers
{
  public class ComponentsController : Controller
  {

    private readonly ElectronicsVendorContext _db;
    public ComponentsController(ElectronicsVendorContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Component> model = _db.Components.ToList(); 
        model = _db.Components.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      // ViewBag.CategoryId = new SelectList(_db.Vendors, "VendorId", "Name");
      ViewBag.Manufacturers = _db.Manufacturers;
      ViewBag.Types = _db.Types;
      return View();
    }

    [HttpPost]
    public ActionResult Create(Component component)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.VendorId = new SelectList(_db.Vendors, "VendorId", "Name");
        return View(component);
      }
      else
      {
        _db.Components.Add(component);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id)
    {
      Component thisComponent = _db.Components
          .Include(component => component.JoinEntities)
          .ThenInclude(join => join.Vendor)
          .FirstOrDefault(component => component.ComponentId == id);
      return View(thisComponent);
    }

    public ActionResult Edit(int id)
    {
      Component thisComponent = _db.Components
            .Include(component => component.JoinEntities)
            .ThenInclude(join => join.Vendor)
            .FirstOrDefault(component => component.ComponentId == id);
      return View(thisComponent);
    }

    [HttpPost]
    public ActionResult Edit(Component component)
    {
      _db.Components.Update(component);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Component thisComponent = _db.Components.FirstOrDefault(component => component.ComponentId == id);
      return View(thisComponent);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Component thisComponent = _db.Components.FirstOrDefault(component => component.ComponentId == id);
      _db.Components.Remove(thisComponent);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddVendor(int id)
    {
      Component thisComponent = _db.Components.FirstOrDefault(component => component.ComponentId == id);
      ViewBag.VendorId = new SelectList(_db.Vendors, "VendorId", "Name");
      return View(thisComponent);
    }

    [HttpPost]
    public ActionResult AddVendor(Component component, int vendorId)
    {
#nullable enable
      ComponentVendor? joinEntity = _db.ComponentVendors.FirstOrDefault(join => (join.VendorId == vendorId && join.ComponentId == component.ComponentId));
#nullable disable
      if (joinEntity == null && vendorId != 0)
      {
        _db.ComponentVendors.Add(new ComponentVendor() { VendorId = vendorId, ComponentId = component.ComponentId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = component.ComponentId });
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      ComponentVendor joinEntry = _db.ComponentVendors.FirstOrDefault(entry => entry.ComponentVendorId == joinId);
      _db.ComponentVendors.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }

}