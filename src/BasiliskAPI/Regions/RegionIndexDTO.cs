using BasiliskAPI.Salesman;

namespace BasiliskAPI.Regions;

public class RegionIndexDTO
{
    public List<RegionDTO> Regions { get; set; }
    public PaginationDTO Pagination { get; set; }
    public string City { get; set; }
}