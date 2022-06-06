namespace TopWay.API.TopWay.Domain.Models;

public class Favorite
{
    public int Id { get; set; }
    public ClimbingGym ClimbingGym { get; set; }
    public int ClimbingGymId { get; set; }
    public Scaler Scaler { get; set; }
    public int ScalerId { get; set; }
}