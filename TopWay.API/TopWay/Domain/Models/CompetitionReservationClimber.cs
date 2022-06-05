namespace TopWay.API.TopWay.Domain.Models;

public class CompetitionReservationClimber
{
    public int Id { get; set; }
    public string Status { get; set; }
    public CompetitionGym CompetitionGym { get; set; }
    public int CompetitionGymId { get; set; }
    public Scaler Scaler { get; set; }
    public int ScalerId { get; set; }
}