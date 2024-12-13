namespace BasiliskWeb.ViewModels.Supplier;

public class SupplierViewModel
{
    public long Id { get; set; }
    public string CompanyName { get; set; }
    public string ContactPerson { get; set; }
    public string JobTitle { get; set; }
    public DateTime? DeleteDate { get; set; }
}