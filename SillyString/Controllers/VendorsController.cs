using Microsoft.AspNetCore.Mvc;
using ElectronicsVendor.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectronicsVendor.Controllers
{
  public class VendorsController : Controller
  {
    private readonly ElectronicsVendorContext _db;

    public VendorsController(ElectronicsVendorContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Vendors.ToList());
    }

    public ActionResult Details(int id)
    {
      Vendor thisVendor = _db.Vendors
          .Include(vendor => vendor.JoinEntities)
          .ThenInclude(join => join.Component)
          .FirstOrDefault(vendor => vendor.VendorId == id);
      return View(thisVendor);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Vendor vendor)
    {
      _db.Vendors.Add(vendor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddComponent(int id)
    {
      Vendor thisVendor = _db.Vendors.FirstOrDefault(vendors => vendors.VendorId == id);
      ViewBag.ComponentId = new SelectList(_db.Components, "ComponentId", "Name", "Name");
      return View(thisVendor);
    }
// ------------------------------------------------------------------

    [HttpPost]
    public ActionResult AddComponent(Vendor vendor, int componentId)
    {
#nullable enable
      ComponentVendor? joinEntity = _db.ComponentVendors.FirstOrDefault(join => (join.ComponentId == componentId && join.VendorId == vendor.VendorId));
      #nullable disable
      if (joinEntity == null && componentId != 0)
      {
        _db.ComponentVendors.Add(new ComponentVendor() { ComponentId = componentId, VendorId = vendor.VendorId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = vendor.VendorId });
    }

    public ActionResult Edit(int id)
    {
      Vendor thisVendor = _db.Vendors.FirstOrDefault(vendors => vendors.VendorId == id);
      return View(thisVendor);
    }

    [HttpPost]
    public ActionResult Edit(Vendor vendor)
    {
      _db.Vendors.Update(vendor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

        public ActionResult Delete(int id)
    {
      Vendor thisVendor = _db.Vendors.FirstOrDefault(vendors => vendors.VendorId == id);
      return View(thisVendor);
    }

        [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Vendor thisVendor = _db.Vendors.FirstOrDefault(vendors => vendors.VendorId == id);
      _db.Vendors.Remove(thisVendor);
      _db.SaveChanges();
      return RedirectToAction("Index");
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