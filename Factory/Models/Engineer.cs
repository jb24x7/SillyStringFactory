using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace SillyString.Models
{
  public class Engineer
  {
    public int EngineerId { get; set; }
    [Required(ErrorMessage = "Engineer name cannot be empty. Please enter a name.")]
    public string Name { get; set; }

    public int Price { get; set; }
    public List<EngineerMachine> JoinEntities { get; }
  }
}