using Microsoft.AspNetCore.Mvc;
using SillyString.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SillyString.Controllers
{
  public class EngineersController : Controller
  {

    private readonly SillyStringContext _db;
    public EngineersController(SillyStringContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Engineer> model = _db.Engineers.ToList(); 
        model = _db.Engineers.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      // ViewBag.CategoryId = new SelectList(_db.Machines, "MachineId", "Name");

      return View();
    }

    [HttpPost]
    public ActionResult Create(Engineer egineer)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
        return View(egineer);
      }
      else
      {
        _db.Engineers.Add(egineer);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id)
    {
      Engineer thisEngineer = _db.Engineers
          .Include(egineer => egineer.JoinEntities)
          .ThenInclude(join => join.Machine)
          .FirstOrDefault(egineer => egineer.EngineerId == id);
      return View(thisEngineer);
    }

    public ActionResult Edit(int id)
    {
      Engineer thisEngineer = _db.Engineers
            .Include(egineer => egineer.JoinEntities)
            .ThenInclude(join => join.Machine)
            .FirstOrDefault(egineer => egineer.EngineerId == id);
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult Edit(Engineer egineer)
    {
      _db.Engineers.Update(egineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(egineer => egineer.EngineerId == id);
      return View(thisEngineer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(egineer => egineer.EngineerId == id);
      _db.Engineers.Remove(thisEngineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddMachine(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(egineer => egineer.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult AddMachine(Engineer egineer, int machineId)
    {
#nullable enable
      EngineerMachine? joinEntity = _db.EngineerMachines.FirstOrDefault(join => (join.MachineId == machineId && join.EngineerId == egineer.EngineerId));
#nullable disable
      if (joinEntity == null && machineId != 0)
      {
        _db.EngineerMachines.Add(new EngineerMachine() { MachineId = machineId, EngineerId = egineer.EngineerId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = egineer.EngineerId });
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      EngineerMachine joinEntry = _db.EngineerMachines.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
      _db.EngineerMachines.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }

}