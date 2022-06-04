namespace TopWay.API.TopWay.Domain.Models;

public class CategoryGym
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string CategoryName { get; set; }
    public int ClimbingGymId { get; set; }
    public ClimbingGym ClimbingGym { get; set; }
    public string ClimbingGymName { get; set; }
}