using System.Collections.Generic;
using System;

namespace SillyString.Models
{
  public class Machine
  {
    public int MachineId { get; set; }
    public string Name { get; set; }
    public List<EgineerMachine> JoinEntities { get; }
  }
}