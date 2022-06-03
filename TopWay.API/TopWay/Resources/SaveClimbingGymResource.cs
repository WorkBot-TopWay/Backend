using System.ComponentModel.DataAnnotations;

namespace TopWay.API.TopWay.Resources;

public class SaveClimbingGymResource
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Password { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string City { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string District { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Address { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Phone { get; set; }
    
    [Required]
    [MaxLength(1000)]
    public string LogoUrl { get; set; }
}