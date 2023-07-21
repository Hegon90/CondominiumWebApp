using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CondominiumWebApp.Models;

public partial class Owner
{
    [Key]
    public int OwnerId { get; set; }
    [Required]
    [Display(Name = "Nombre")]
    public string OwnerName { get; set; } = null!;
    [Display(Name = "Apellido")]
    public string OwnerSurname { get; set; } = null!;
    [Display(Name = "Telefono")]
    public int? PhoneNumber { get; set; }

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
