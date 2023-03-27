using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace SillyString.Models
{
  public class Machine
  {
    public int MachineId { get; set; }
    [Required(ErrorMessage = "Machine name cannot be empty. Please enter a name.")]
    public string Name { get; set; }
    public List<EngineerMachine> JoinEntities { get; }
  }
}