namespace TopWay.API.TopWay.Domain.Models;

public class CompetitionGym
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public DateTime Date { get; set; }
    public string type { get; set; }
    public int ClimberGymId { get; set; }
    public ClimbingGym ClimbingGym { get; set; }
    public IList<CompetitionReservationClimber> CompetitionReservationClimbers { get; set; } = new List<CompetitionReservationClimber>();
}