using BasiliskAPI.Salesman;
using BasiliskDataAccess.Models;

namespace BasiliskAPI.Regions;

public class RegionDTO
{
    public long Id { get; set; }
    public string City { get; set; } = null!;
    public string? Remark { get; set; }
}