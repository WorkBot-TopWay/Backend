namespace TopWay.API.TopWay.Domain.Models;

public class Request
{
    public int Id { get; set; }
    public string Status { get; set; }
    public Scaler Scaler { get; set; }
    public int ScalerId { get; set; }
    public League League { get; set; }
    public int LeagueId { get; set; }
}