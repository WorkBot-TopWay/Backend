namespace TopWay.API.TopWay.Domain.Models;

public class CategoryGyms
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public Categories Categories { get; set; }
    public string CategoryName { get; set; }
    public int ClimbingGymId { get; set; }
    public ClimbingGyms ClimbingGyms { get; set; }
    public string ClimbingGymName { get; set; }
}