namespace TopWay.API.TopWay.Domain.Models;

public class CompetitionGyms
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public DateTime Date { get; set; }
    public string type { get; set; }
    public int ClimberGymId { get; set; }
    public ClimbingGyms ClimbingGyms { get; set; }
    public IList<CompetitionReservationClimber> CompetitionReservationClimbers { get; set; } = new List<CompetitionReservationClimber>();
    public IList<CompetitionGymRankings> CompetitionGymRankings { get; set; } = new List<CompetitionGymRankings>();
}