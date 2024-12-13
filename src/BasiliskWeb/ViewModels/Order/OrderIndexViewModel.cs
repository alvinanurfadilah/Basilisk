using Microsoft.AspNetCore.Mvc.Rendering;

namespace BasiliskWeb.ViewModels.Order;

public class OrderIndexViewModel
{
    public List<OrderViewModel> Orders { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string InvoiceNumber { get; set; }
    public long CustomerId { get; set; }
    public string SalesEmployeeNumber { get; set; }
    public long DeliveryId { get; set; }
    public DateTime OrderDate { get; set; }

    public List<SelectListItem> Customers { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> Salesman { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> Deliveries { get; set; } = new List<SelectListItem>();
}
