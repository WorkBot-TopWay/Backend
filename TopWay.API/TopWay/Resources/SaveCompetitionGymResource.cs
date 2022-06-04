using System.ComponentModel.DataAnnotations;

namespace TopWay.API.TopWay.Resources;

public class SaveCompetitionGymResource
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    [MaxLength(50)]
    public string type { get; set; }
}
