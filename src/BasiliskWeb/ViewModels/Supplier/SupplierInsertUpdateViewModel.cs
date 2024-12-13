using System.ComponentModel.DataAnnotations;
using BasiliskWeb.Validations;

namespace BasiliskWeb.ViewModels.Supplier;

public class SupplierInsertUpdateViewModel
{
    public long Id { get; set; }
    [Required]
    [UniqueSupplierCompanyNameValidation]
    public string CompanyName { get; set; } = null!;
    [Required]
    public string ContactPerson { get; set; } = null!;
    [Required]
    public string JobTitle { get; set; } = null!;
    public string? Address { get; set; }
    public string? City { get; set; }
    [Phone]
    public string? Phone { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
}
