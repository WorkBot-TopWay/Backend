using System.ComponentModel.DataAnnotations;

namespace TopWay.API.TopWay.Resources;

public class SaveFeaturesResource
{
    [Required]
    [MaxLength(50)]
    public string Type_climb { get; set; }
    
    [Required]
    public DateTime Office_hours_start { get; set; }
    
    [Required]
    public DateTime Office_hours_end { get; set; }
    
    [Required]
    public int Routes { get; set; }
    
    [Required]
    public float Max_height { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Rock_type { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Bolting { get; set; }
    
    [Required]
    public double price { get; set; }
}