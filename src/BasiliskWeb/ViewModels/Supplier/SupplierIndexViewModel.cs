namespace BasiliskWeb.ViewModels.Supplier;

public class SupplierIndexViewModel
{
    public List<SupplierViewModel> Suppliers { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string CompanyName { get; set; }
    public string ContactPerson { get; set; }
    public string JobTitle { get; set; }
}
