using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BasiliskWeb.ViewModels.Order;

public class OrderDetailInsertUpdateViewModel
{
    public long Id { get; set; }
    public string? InvoiceNumber { get; set; }
    [Required]
    public long ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public decimal Discount { get; set; }

    public List<SelectListItem>? Products { get; set; } = new List<SelectListItem>();
}
