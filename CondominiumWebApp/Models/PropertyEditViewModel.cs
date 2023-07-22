using System.ComponentModel.DataAnnotations;

namespace CondominiumWebApp.Models
{
    public class PropertyEditViewModel
    {
        public int PropertyId { get; set; }

        [Display(Name = "Codigo")]
        public string? PropertyPasscode { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "This field is required.")]
        public string? PropertyType { get; set; }

        [Display(Name = "Propietario")]
        [Required(ErrorMessage = "This field is required.")]
        public int? OwnerId { get; set; }

        [Display(Name = "Fecha Creacion")]
        [Required(ErrorMessage = "This field is required.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PropertyDate { get; set; }
    }
}
