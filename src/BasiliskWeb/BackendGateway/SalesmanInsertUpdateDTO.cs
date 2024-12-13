using System;
using System.ComponentModel.DataAnnotations;

namespace BasiliskWeb.BackendGateway;

public class SalesmanInsertUpdateDTO
{
    [Required]
    public string EmployeeNumber { get; set; } = null!;
    [Required]
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    [Required]
    public string Level { get; set; } = null!;
    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    public DateTime HiredDate { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Phone { get; set; }
    public string? SuperiorEmployeeNumber { get; set; }
}
