namespace TopWay.API.TopWay.Resources;

public class CompetitionGymRankingResource
{
    public int Id { get; set; }
    public double Score { get; set; }
    public int CompetitionGymId { get; set; }
    public int ScalerId { get; set; }
}