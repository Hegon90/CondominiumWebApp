using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CondominiumWebApp.Models;

public partial class Street
{
    [Key]
    public int StreetId { get; set; }

    public int StreetNumber { get; set; }

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
