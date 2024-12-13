namespace BasiliskWeb.ViewModels.Delivery;

public class DeliveryIndexViewModel
{
    public List<DeliveryViewModel> Deliveries { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string CompanyName { get; set; }
}
