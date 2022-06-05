namespace TopWay.API.TopWay.Resources;

public class CompetitionLeagueResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string type { get; set; }
    public int LeagueId { get; set; }
}