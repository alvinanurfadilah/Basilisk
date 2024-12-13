using System.ComponentModel.DataAnnotations;
using BasiliskDataAccess.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BasiliskWeb;

public class ProductInsertUpdateViewModel
{
    public long Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public long CategoryId { get; set; }
    [Required]
    public long? SupplierId { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public decimal Price { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int Stock { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int OnOrder { get; set; }
    public bool Discontinue { get; set; }
    public string? Description { get; set; }

    public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> Suppliers { get; set; } = new List<SelectListItem>();
}
