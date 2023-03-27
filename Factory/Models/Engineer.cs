using System.Collections.Generic;
using System;

namespace SillyString.Models
{
  public class Engineer
  {
    public int EngineerId { get; set; }
    public string Name { get; set; }

    public int Price { get; set; }
    public List<EngineerMachine> JoinEntities { get; }
  }
}