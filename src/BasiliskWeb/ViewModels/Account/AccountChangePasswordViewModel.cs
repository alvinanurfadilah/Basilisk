using System;
using System.ComponentModel.DataAnnotations;

namespace BasiliskWeb.ViewModels.Account;

public class AccountChangePasswordViewModel
{
    public string? Username { get; set; }
    [Required]
    public string OldPassword { get; set; }
    [Required]
    public string NewPassword { get; set; }
    [Compare("NewPassword", ErrorMessage = "Confirm Passord do not match!")]
    public string ConfirmPassword { get; set; }
}
