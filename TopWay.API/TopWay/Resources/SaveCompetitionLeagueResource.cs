using System.ComponentModel.DataAnnotations;

namespace TopWay.API.TopWay.Resources;

public class SaveCompetitionLeagueResource
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    public string type { get; set; }
}