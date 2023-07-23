using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CondominiumWebApp.Models;

public partial class User
{
    [Key]
    public int UserId { get; set; }
    [Display(Name = "Nombre de Usuario")]
    [Required(ErrorMessage = "This field is required.")]
    public string UserNickname { get; set; } = null!;
    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "This field is required.")]
    public string UserName { get; set; } = null!;
    [Display(Name = "Apellido")]
    public string? UserSurname { get; set; }
    [Display(Name = "Correo")]
    [Required(ErrorMessage = "This field is required.")]
    public string UserEmail { get; set; } = null!;
    [Display(Name = "Contraseña")]
    [Required(ErrorMessage = "This field is required.")]
    public string UserPassword { get; set; } = null!;
}
