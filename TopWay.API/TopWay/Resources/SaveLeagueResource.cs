using System.ComponentModel.DataAnnotations;

namespace TopWay.API.TopWay.Resources;

public class SaveLeagueResource
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(500)]
    public string UrlLogo { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
}