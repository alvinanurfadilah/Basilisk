namespace BasiliskWeb.ViewModels.Order;

public class OrderViewModel
{
    public string InvoiceNumber { get; set; } = null!;
    public string CustomerCompanyName { get; set; }
    public string SalesmanName { get; set; }
    public DateTime OrderDate { get; set; }
    public string CompanyName { get; set; } = null!;
}
