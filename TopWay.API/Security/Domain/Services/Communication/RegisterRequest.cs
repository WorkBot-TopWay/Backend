using System.ComponentModel.DataAnnotations;

namespace TopWay.API.Security.Domain.Services.Communication;

public class RegisterRequest
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string City { get; set; }
    
    [Required]
    public string District { get; set; }
    
    [Required]
    public string Address { get; set; }
    
    [Required]
    public string Phone { get; set; }
    
    [Required]
    public string UrlPhoto { get; set; }
}