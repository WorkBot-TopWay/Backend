using System.Text.Json.Serialization;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.Security.Domain.Models;

public class Scaler
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [JsonIgnore] 
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string UrlPhoto { get; set; }
    public string Type { get; set; }
    public IList<Notification> Notifications { get; set; } = new List<Notification>();
    public IList<Comments> Comments { get; set; } = new List<Comments>();
    public IList<CompetitionReservationClimber> CompetitionReservationClimbers { get; set; } = new List<CompetitionReservationClimber>();
    public IList<CompetitionGymRankings> CompetitionGymRankings { get; set; } = new List<CompetitionGymRankings>();
    public IList<League> Leagues { get; set; } = new List<League>();
    public IList<Request> Requests { get; set; } = new List<Request>();
    public IList<ClimberLeagues> ClimbersLeagues { get; set; } = new List<ClimberLeagues>();
    public IList<CompetitionLeagueRanking> CompetitionLeagueRankings { get; set; } = new List<CompetitionLeagueRanking>();
    public IList<Favorite> Favorites { get; set; } = new List<Favorite>();
}