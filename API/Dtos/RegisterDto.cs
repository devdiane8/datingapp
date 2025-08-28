using System;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class RegisterDto
{
    [Required]
    public string DisplayName { get; set; } = "";
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";
    [Required]
    [MinLength(4, ErrorMessage = "Password must be at least 4 characters long.")]
    public string Password { get; set; } = "";

    [Required]
    public string Gender { get; set; } = "";
    [Required]
    public DateOnly DateOfBirth { get; set; }
    [Required]
    public string City { get; set; } = "";
    [Required]
    public string Country { get; set; } = "";


}
