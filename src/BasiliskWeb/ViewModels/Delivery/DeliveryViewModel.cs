namespace BasiliskWeb.ViewModels.Delivery;

public class DeliveryViewModel
{
    public long Id { get; set; }
    public string CompanyName { get; set; } = null!;
    public string? Phone { get; set; }
    public decimal Cost { get; set; }
}
