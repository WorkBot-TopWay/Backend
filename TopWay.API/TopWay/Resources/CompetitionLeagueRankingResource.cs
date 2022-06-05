namespace TopWay.API.TopWay.Resources;

public class CompetitionLeagueRankingResource
{
    public int Id { get; set; }
    public double Score { get; set; }
    public int CompetitionLeagueId { get; set; }
    public int ScalerId { get; set; }
}