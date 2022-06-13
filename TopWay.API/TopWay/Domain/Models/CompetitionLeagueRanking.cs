using TopWay.API.Security.Domain.Models;

namespace TopWay.API.TopWay.Domain.Models;

public class CompetitionLeagueRanking
{
    public int Id { get; set; }
    public double Score { get; set; }
    public CompetitionLeague CompetitionLeague { get; set; }
    public int CompetitionLeagueId { get; set; }
    public Scaler Scaler { get; set; }
    public int ScalerId { get; set; }
}