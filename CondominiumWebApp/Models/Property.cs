using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CondominiumWebApp.Models;

public partial class Property
{
    public int PropertyId { get; set; }
    
    [Display(Name = "Codigo")]
    public string? PropertyPasscode { get; set; }
    
    [Display(Name = "Categoria")]
    [Required(ErrorMessage = "This field is required.")]
    public string PropertyType { get; set; } = null!;

    [Display(Name = "Bloque")]
    [Required(ErrorMessage = "This field is required.")]
    public int? BlockId { get; set; }
    
    [Display(Name = "Calle")]
    [Required(ErrorMessage = "This field is required.")]
    public int? StreetId { get; set; }

    [Display(Name = "Numero")]
    [Required(ErrorMessage = "This field is required.")]
    public int? PropertyNumber { get; set; }

    [Display(Name = "Propietario")]
    [Required(ErrorMessage = "This field is required.")]
    public int? OwnerId { get; set; }

    [Display(Name = "Fecha Creacion")]
    [Required(ErrorMessage = "This field is required.")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? PropertyDate { get; set; }

    public virtual Block Block { get; set; } = null!;

    public virtual Owner? Owner { get; set; }

    public virtual Street Street { get; set; } = null!;
}
