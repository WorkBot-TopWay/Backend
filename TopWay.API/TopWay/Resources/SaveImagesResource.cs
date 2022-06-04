using System.ComponentModel.DataAnnotations;

namespace TopWay.API.TopWay.Resources;

public class SaveImagesResource
{
    [Required]
    [MaxLength(500)]
    public string ImageUrl { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Alt { get; set; }
}