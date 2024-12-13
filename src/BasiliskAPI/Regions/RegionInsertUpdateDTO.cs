using System.ComponentModel.DataAnnotations;

namespace BasiliskAPI.Regions;

public class RegionInsertUpdateDTO
{
    public long Id { get; set; }
    [Required]
    public string City { get; set; } = null!;
    public string? Remark { get; set; }
}