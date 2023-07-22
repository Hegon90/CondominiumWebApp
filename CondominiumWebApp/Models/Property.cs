using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CondominiumWebApp.Models;

public partial class Property
{
    public int PropertyId { get; set; }
    
    [Display(Name = "Codigo")]
    public string? PropertyPasscode { get; set; }
    
    [Display(Name = "Categoria")]
    public string PropertyType { get; set; } = null!;

    [Display(Name = "Bloque")]
    public int? BlockId { get; set; }
    
    [Display(Name = "Calle")]
    public int? StreetId { get; set; }


    [Display(Name = "Propietario")]
    public int? OwnerId { get; set; }

    [Display(Name = "Fecha Creacion")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? PropertyDate { get; set; }

    public virtual Block Block { get; set; } = null!;

    public virtual Owner? Owner { get; set; }

    public virtual Street Street { get; set; } = null!;

    [NotMapped]
    [Display(Name = "Numero")]
    public int PropertyNumber { get; set; }
}
