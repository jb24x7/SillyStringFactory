using Microsoft.AspNetCore.Mvc;
using SillyString.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SillyString.Controllers
{
  public class MachinesController : Controller
  {
    private readonly SillyStringContext _db;

    public MachinesController(SillyStringContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Machines.ToList());
    }

    public ActionResult Details(int id)
    {
      Machine thisMachine = _db.Machines
          .Include(machine => machine.JoinEntities)
          .ThenInclude(join => join.Egineer)
          .FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Machine machine)
    {
      _db.Machines.Add(machine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddEgineer(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineId == id);
      ViewBag.EgineerId = new SelectList(_db.Egineers, "EgineerId", "Name", "Name");
      return View(thisMachine);
    }
// ------------------------------------------------------------------

    [HttpPost]
    public ActionResult AddEgineer(Machine machine, int egineerId)
    {
#nullable enable
      EgineerMachine? joinEntity = _db.EgineerMachines.FirstOrDefault(join => (join.EgineerId == egineerId && join.MachineId == machine.MachineId));
      #nullable disable
      if (joinEntity == null && egineerId != 0)
      {
        _db.EgineerMachines.Add(new EgineerMachine() { EgineerId = egineerId, MachineId = machine.MachineId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = machine.MachineId });
    }

    public ActionResult Edit(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult Edit(Machine machine)
    {
      _db.Machines.Update(machine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

        public ActionResult Delete(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineId == id);
      return View(thisMachine);
    }

        [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineId == id);
      _db.Machines.Remove(thisMachine);
      _db.SaveChanges();
      return RedirectToAction("Index");
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