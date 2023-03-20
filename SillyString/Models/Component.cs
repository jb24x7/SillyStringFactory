using System.Collections.Generic;
using System;

namespace ElectronicsVendor.Models
{
  public class Component
  {
    public int ComponentId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Manufacturer { get; set; }
    public int Price { get; set; }
    public List<ComponentVendor> JoinEntities { get; }
  }
}