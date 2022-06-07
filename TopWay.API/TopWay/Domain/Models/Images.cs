namespace TopWay.API.TopWay.Domain.Models;

public class Images
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public string Alt { get; set; }
    public ClimbingGyms ClimbingGyms { get; set; }
    public int ClimbingGymId { get; set; }
}