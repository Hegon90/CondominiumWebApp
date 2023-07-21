using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CondominiumWebApp.Models;

public partial class Property
{
    [Key]
    public int PropertyId { get; set; }
    [Required]
    [Display(Name = "Codigo")]
    public string PropertyPasscode { get; set; } = null!;
    [Required]
    [Display(Name = "Categoria")]
    public string PropertyType { get; set; } = null!;
    [Required]
    [Display(Name = "Bloque")]
    public int BlockId { get; set; }
    [Required]
    [Display(Name = "Calle")]
    public int StreetId { get; set; }
    [Required]
    [Display(Name = "Propietario")]
    public int? OwnerId { get; set; }
    [Required]
    [Display(Name = "Fecha Creacion")]
    public DateOnly PropertyDate { get; set; }

    public virtual Block Block { get; set; } = null!;

    public virtual Owner? Owner { get; set; }

    public virtual Street Street { get; set; } = null!;
}
