using System;

namespace BasiliskAPI.Salesman;

public class SalesmanGetByIdDTO
{
    public string EmployeeNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string Level { get; set; } = null!;
    public string BirthDate { get; set; }
    public string HiredDate { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Phone { get; set; }
    public string? SuperiorEmployeeNumber { get; set; }
}
