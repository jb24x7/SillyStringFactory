using System.Collections.Generic;
using System;

namespace ElectronicsVendor.Models
{
  public class Vendor
  {
    public int VendorId { get; set; }
    public string Name { get; set; }
    public List<ComponentVendor> JoinEntities { get; }
  }
}