using System.ComponentModel.DataAnnotations;

namespace TopWay.API.TopWay.Resources;

public class SaveNewsResource
{
    [Required]
    [MaxLength(50)]
    public string Title { get; set; }
    
    [Required]
    [MaxLength(500)]
    public string description { get; set; }

    [Required]
    [MaxLength(100)]
    public string ImageUrl { get; set; }
}