namespace TopWay.API.TopWay.Domain.Models;

public class League
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UrlLogo { get; set; }
    public string Description { get; set; }
    public string AdminName { get; set; }
    public int NumberParticipants { get; set; }
    public Scaler Scaler { get; set; }
    public int ScalerId { get; set; }
    public ClimbingGyms ClimbingGyms { get; set; }
    public int ClimbingGymId { get; set; }
    public IList<Request> Requests { get; set; } = new List<Request>();
    public IList<ClimberLeagues> ClimbersLeagues { get; set; } = new List<ClimberLeagues>();
    public IList<CompetitionLeague> CompetitionLeagues { get; set; } = new List<CompetitionLeague>();
}