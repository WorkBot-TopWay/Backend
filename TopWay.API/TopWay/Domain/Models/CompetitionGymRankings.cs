namespace TopWay.API.TopWay.Domain.Models;

public class CompetitionGymRankings
{
    public int Id { get; set; }
    public double Score { get; set; }
    public CompetitionGyms CompetitionGyms { get; set; }
    public int CompetitionGymId { get; set; }
    public Scaler Scaler { get; set; }
    public int ScalerId { get; set; }
}