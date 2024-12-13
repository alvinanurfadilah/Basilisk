using System.ComponentModel.DataAnnotations;
using BasiliskWeb.Validations;

namespace BasiliskWeb.ViewModels.Delivery;

public class DeliveryInsertUpdateViewModel
{
    public long Id { get; set; }
    [Required]
    [UniqueDeliveryCompanyNameValidation]
    public string CompanyName { get; set; } = null!;
    [Phone]
    public string? Phone { get; set; }
    public decimal Cost { get; set; }
}
