using System;
using BasiliskAPI.Regions;

namespace BasiliskAPI.Salesman;

public class SalesmanRegionDTO
{
    public string EmployeeNumber { get; set; }
    public string FullName { get; set; }
    public RegionIndexDTO SalesmanRegion { get; set; }
}
