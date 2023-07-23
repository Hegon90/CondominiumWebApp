using System.ComponentModel.DataAnnotations;

namespace CondominiumWebApp.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        [Display(Name = "Area")]
        [Required(ErrorMessage = "This field is required.")]
        public string? BookingPlace { get; set; }
        [Display(Name = "Dia")]
        [Required(ErrorMessage = "This field is required.")]
        public string? BookingDay { get; set; }
        [Display(Name = "Hora")]
        [Required(ErrorMessage = "This field is required.")]
        public string? BookingTime { get; set; }
        [Display(Name = "Responsable")]
        [Required(ErrorMessage = "This field is required.")]
        public string? BookingName { get; set; }
    }
}
