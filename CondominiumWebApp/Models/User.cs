using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CondominiumWebApp.Models;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    public string UserNickname { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string? UserSurname { get; set; }

    public string UserEmail { get; set; } = null!;

    public string UserPassword { get; set; } = null!;
}
