using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CondominiumWebApp.Models;

public partial class Block
{
    [Key]
    public int BlockId { get; set; }
    [Display(Name = "Bloque")]
    public string BlockName { get; set; } = null!;

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
