namespace BasiliskAPI.Salesman;

public class SalesmanIndexDTO
{
    public List<SalesmanDTO> Salesman { get; set; }
    // public SalesmanDTO? Superior { get; set; }
    public PaginationDTO Pagination { get; set; }
    public long RegionId { get; set; }
    public string? EmployeeNumber { get; set; }
    public string? FullName { get; set; }
    public string? Level { get; set; }
    public string? SuperiorName { get; set; }
}
