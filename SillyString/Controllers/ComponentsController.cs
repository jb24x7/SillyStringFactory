using Microsoft.AspNetCore.Mvc;
using SillyString.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SillyString.Controllers
{
  public class EgineersController : Controller
  {

    private readonly SillyStringContext _db;
    public EgineersController(SillyStringContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Egineer> model = _db.Egineers.ToList(); 
        model = _db.Egineers.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      // ViewBag.CategoryId = new SelectList(_db.Machines, "MachineId", "Name");

      return View();
    }

    [HttpPost]
    public ActionResult Create(Egineer egineer)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
        return View(egineer);
      }
      else
      {
        _db.Egineers.Add(egineer);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id)
    {
      Egineer thisEgineer = _db.Egineers
          .Include(egineer => egineer.JoinEntities)
          .ThenInclude(join => join.Machine)
          .FirstOrDefault(egineer => egineer.EgineerId == id);
      return View(thisEgineer);
    }

    public ActionResult Edit(int id)
    {
      Egineer thisEgineer = _db.Egineers
            .Include(egineer => egineer.JoinEntities)
            .ThenInclude(join => join.Machine)
            .FirstOrDefault(egineer => egineer.EgineerId == id);
      return View(thisEgineer);
    }

    [HttpPost]
    public ActionResult Edit(Egineer egineer)
    {
      _db.Egineers.Update(egineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Egineer thisEgineer = _db.Egineers.FirstOrDefault(egineer => egineer.EgineerId == id);
      return View(thisEgineer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Egineer thisEgineer = _db.Egineers.FirstOrDefault(egineer => egineer.EgineerId == id);
      _db.Egineers.Remove(thisEgineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddMachine(int id)
    {
      Egineer thisEgineer = _db.Egineers.FirstOrDefault(egineer => egineer.EgineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
      return View(thisEgineer);
    }

    [HttpPost]
    public ActionResult AddMachine(Egineer egineer, int machineId)
    {
#nullable enable
      EgineerMachine? joinEntity = _db.EgineerMachines.FirstOrDefault(join => (join.MachineId == machineId && join.EgineerId == egineer.EgineerId));
#nullable disable
      if (joinEntity == null && machineId != 0)
      {
        _db.EgineerMachines.Add(new EgineerMachine() { MachineId = machineId, EgineerId = egineer.EgineerId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = egineer.EgineerId });
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      EgineerMachine joinEntry = _db.EgineerMachines.FirstOrDefault(entry => entry.EgineerMachineId == joinId);
      _db.EgineerMachines.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }

}