using System.ComponentModel.DataAnnotations;
using BasiliskWeb.Validations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BasiliskWeb.ViewModels.Account;

public class AccountRegisterViewModel
{
    [UniqueUsernameValidation]
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Compare("Password", ErrorMessage = "Password are not the same")]
    public string ConfirmPassword { get; set; } = null!;
    [Required]
    public string Role { get; set; } = null!;

    public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
}