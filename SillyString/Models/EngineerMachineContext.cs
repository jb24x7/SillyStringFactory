using Microsoft.EntityFrameworkCore;

namespace SillyString.Models
{
  public class SillyStringContext : DbContext
  {
    public DbSet<Machine> Machines { get; set; }
    public DbSet<Egineer> Egineers { get; set; }
    public DbSet<EgineerMachine> EgineerMachines { get; set; }
    public SillyStringContext(DbContextOptions options) : base(options) { }
  }
}