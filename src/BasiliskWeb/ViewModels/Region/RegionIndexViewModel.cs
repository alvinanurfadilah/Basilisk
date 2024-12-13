namespace BasiliskWeb.ViewModels.Region;

public class RegionIndexViewModel
{
    public List<RegionViewModel> Regions { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string City { get; set; }
}
