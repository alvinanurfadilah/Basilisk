using System.ComponentModel.DataAnnotations;

namespace BasiliskWeb.ViewModels.Customer;

public class CustomerInsertUpdateViewModel
{
    public long Id { get; set; }
    [Required]
    public string CompanyName { get; set; } = null!;
    [Required]
    public string ContactPerson { get; set; } = null!;
    public string? Address { get; set; }
    public string? City { get; set; }
    [Phone]
    public string? Phone { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
}
