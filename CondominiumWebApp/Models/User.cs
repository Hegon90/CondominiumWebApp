using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CondominiumWebApp.Models;

public partial class User
{
    [Key]
    public int UserId { get; set; }
    [Required]
    public string UserNickname { get; set; } = null!;
    [Required]
    public string UserName { get; set; } = null!;

    public string? UserSurname { get; set; }
    [Required]
    public string UserEmail { get; set; } = null!;
    [Required]
    public string UserPassword { get; set; } = null!;
}
