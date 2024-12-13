using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BasiliskWeb.ViewModels.Order;

public class OrderInsertUpdateViewModel
{
    public string InvoiceNumber { get; set; } = "22-02-0002";
    [Required]
    public long CustomerId { get; set; }
    [Required]
    public string SalesEmployeeNumber { get; set; } = null!;
    [Required]
    public DateTime OrderDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public DateTime? DueDate { get; set; }
    [Required]
    public long DeliveryId { get; set; }
    [Required]
    public string DestinationAddress { get; set; } = null!;
    [Required]
    public string DestinationCity { get; set; } = null!;
    [Required]
    public string DestinationPostalCode { get; set; } = null!;

    public List<SelectListItem>? Customers { get; set; } = new List<SelectListItem>();
    public List<SelectListItem>? Salesman { get; set; } = new List<SelectListItem>();
    public List<SelectListItem>? Deliveries { get; set; } = new List<SelectListItem>();
}
