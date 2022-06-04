namespace TopWay.API.TopWay.Domain.Models;

public class ClimbingGym
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string LogoUrl { get; set; }
    public string type { get; set; }
    
    public IList<CategoryGym> CategoryGyms { get; set; } = new List<CategoryGym>();
    public IList<Images> Images { get; set; } = new List<Images>();
}