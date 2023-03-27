using Microsoft.EntityFrameworkCore;

namespace SillyString.Models
{
  public class SillyStringContext : DbContext
  {
    public DbSet<Machine> Machines { get; set; }
    public DbSet<Engineer> Engineers { get; set; }
    public DbSet<EngineerMachine> EngineerMachines { get; set; }
    public SillyStringContext(DbContextOptions options) : base(options) { }
  }
}