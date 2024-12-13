namespace BasiliskAPI.Salesman;

public class SalesmanDTO
{
    public string EmployeeNumber { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Level { get; set; } = null!;
    public string? Superior { get; set; }
}
