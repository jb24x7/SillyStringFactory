namespace SillyString.Models
{
public class EgineerMachine
  {
    public int EgineerMachineId { get; set; }
    public int EgineerId { get; set; }
    public int MachineId { get; set; }
    public Egineer Egineer { get; set; }
    public Machine Machine { get; set; }
  }
}