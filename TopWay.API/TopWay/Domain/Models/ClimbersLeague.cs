namespace TopWay.API.TopWay.Domain.Models;

public class ClimbersLeague
{
    public int Id { get; set; }
    public Scaler Scaler { get; set; }
    public int ScalerId { get; set; }
    public ClimbingGym ClimbingGym { get; set; }
    public int ClimbingGymId { get; set; }
    public League League { get; set; }
    public int LeagueId { get; set; }
}