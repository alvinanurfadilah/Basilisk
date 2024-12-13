using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BasiliskWeb.ViewModels.Account;

public class AccountLoginViewModel
{
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Required]
    public string Role { get; set; } = null!;

    public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();

}