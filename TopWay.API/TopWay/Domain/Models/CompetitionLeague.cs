namespace TopWay.API.TopWay.Domain.Models;

public class CompetitionLeague
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string type { get; set; }
    public League League { get; set; }
    public int LeagueId { get; set; }
}