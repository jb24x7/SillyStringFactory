using Microsoft.EntityFrameworkCore;

namespace ElectronicsVendor.Models
{
  public class ElectronicsVendorContext : DbContext
  {
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<Type> Types { get; set; }
    public DbSet<ComponentVendor> ComponentVendors { get; set; }
    public ElectronicsVendorContext(DbContextOptions options) : base(options) { }
  }
}