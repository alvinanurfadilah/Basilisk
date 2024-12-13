namespace BasiliskWeb.ViewModels.Customer;

public class CustomerViewModel
{
    public long Id { get; set; }
    public string CompanyName { get; set; } = null!;
    public string ContactPerson { get; set; } = null!;
    public string? Address { get; set; }
    public string? City { get; set; }
    public DateTime? DeleteDate { get; set; }
}