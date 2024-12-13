namespace BasiliskWeb.ViewModels.Salesman;

public class SalesmanViewModel
{
    public string EmployeeNumber { get; set; } = null!;
    public string FullName { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string Level { get; set; } = null!;
    public string? SuperiorEmployeeNumber { get; set; }
}