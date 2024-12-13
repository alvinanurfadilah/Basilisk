namespace BasiliskWeb.ViewModels.Salesman;

public class SalesmanFormViewModel
{
    public string EmployeeNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string Level { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public DateTime HiredDate { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Phone { get; set; }
    public string? SuperiorEmployeeNumber { get; set; }
}
