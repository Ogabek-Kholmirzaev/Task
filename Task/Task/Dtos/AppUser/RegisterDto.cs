using System.ComponentModel.DataAnnotations;

namespace Taskk.Dtos.AppUser;

public class RegisterDto
{
    [Display(Name = "Full name")]
    [Required]
    public string? FullName { get; set; }

    [Display(Name = "UserName")]
    [Required]
    [MinLength(4)]
    public string? UserName { get; set; }

    [Display(Name = "Role")]
    [Required]
    public string? Role { get; set; }

    [Display(Name = "Email address")]
    [Required]
    public string? EmailAddress { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(8)]
    public string? Password { get; set; }

    [Display(Name = "Confirm password")]
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Password doesn't equal to confirm password")]
    public string? ConfirmPassword { get; set; }
}