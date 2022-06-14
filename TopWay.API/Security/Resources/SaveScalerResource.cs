using System.ComponentModel.DataAnnotations;

namespace TopWay.API.Security.Resources;

public class SaveScalerResource
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(150)]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string City { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string District { get; set; }
    
    
    [Required]
    [MaxLength(250)]
    public string Address { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string Phone { get; set; }
    
    [Required]
    [MaxLength(1000)]
    public string UrlPhoto { get; set; }
}