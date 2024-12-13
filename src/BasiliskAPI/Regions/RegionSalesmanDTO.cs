using BasiliskAPI.Salesman;

namespace BasiliskAPI;

public class RegionSalesmanDTO
{
    public string City { get; set; }
    public string Remark { get; set; }
    // public List<SalesmanDTO> SalesmanRegion { get; set; }
    public SalesmanIndexDTO RegionSalesman { get; set; }
    // public PaginationDTO Pagination { get; set; }
    // public string? EmployeeNumber { get; set; }
    // public string? FullName { get; set; }
    // public string? Level { get; set; }
    // public string? SuperiorName { get; set; }
}