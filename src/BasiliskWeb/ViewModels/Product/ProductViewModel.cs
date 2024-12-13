using BasiliskDataAccess.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BasiliskWeb;

public class ProductViewModel
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? CategoryName { get; set; }
    public string? CompanyName { get; set; }
    public decimal Price { get; set; }
}