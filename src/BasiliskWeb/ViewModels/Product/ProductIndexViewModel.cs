using Microsoft.AspNetCore.Mvc.Rendering;

namespace BasiliskWeb.ViewModels.Product;

public class ProductIndexViewModel
{
    public List<ProductViewModel> Products { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string? Name { get; set; }
    public long CategoryId { get; set; }
    public long SupplierId { get; set; }

    public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> Suppliers { get; set; } = new List<SelectListItem>();
}