using System;

namespace BasiliskAPI.Regions;

public class RegionSalesmanInsertDTO
{
    public long RegionId { get; set; }
    public List<string> Salesman { get; set; }
}
