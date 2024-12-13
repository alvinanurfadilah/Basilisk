using System.ComponentModel.DataAnnotations;
using BasiliskWeb.Validations;

namespace BasiliskWeb.ViewModels.Category;

public class CategoryInsertUpdateViewModel
{
    public long Id { get; set; }
    [Required]
    [UniqueCategoryNameValidation]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}