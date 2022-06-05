namespace TopWay.API.TopWay.Resources;

public class CompetitionReservationClimberResource
{
    public int Id { get; set; }
    public string Status { get; set; }
    public int CompetitionGymId { get; set; }
    public int ScalerId { get; set; }
}