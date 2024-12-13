namespace BasiliskWeb.ViewModels.Category;

public class CategoryViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ProductViewModel> Products { get; set; }
}
