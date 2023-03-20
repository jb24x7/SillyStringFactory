namespace ElectronicsVendor.Models
{
public class ComponentVendor
  {
    public int ComponentVendorId { get; set; }
    public int ComponentId { get; set; }
    public int VendorId { get; set; }
    public Component Component { get; set; }
    public Vendor Vendor { get; set; }
  }
}