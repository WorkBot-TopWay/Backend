namespace TopWay.API.TopWay.Domain.Models;

public class News
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public DateTime Date { get; set; }
    
    public string ImageUrl { get; set; }
    
    public ClimbingGym ClimbingGym { get; set; }
    
    public int ClimbingGymId { get; set; }
}