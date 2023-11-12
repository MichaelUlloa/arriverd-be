using System.ComponentModel.DataAnnotations;

namespace arriverd_be.Models;

public class RegisterModel
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [EmailAddress]
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
