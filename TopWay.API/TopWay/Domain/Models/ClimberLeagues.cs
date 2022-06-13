using TopWay.API.Security.Domain.Models;

namespace TopWay.API.TopWay.Domain.Models;

public class ClimberLeagues
{
    public int Id { get; set; }
    public Scaler Scaler { get; set; }
    public int ScalerId { get; set; }
    public ClimbingGyms ClimbingGyms { get; set; }
    public int ClimbingGymId { get; set; }
    public League League { get; set; }
    public int LeagueId { get; set; }
}