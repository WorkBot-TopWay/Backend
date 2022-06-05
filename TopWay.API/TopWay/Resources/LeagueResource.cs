namespace TopWay.API.TopWay.Resources;

public class LeagueResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UrlLogo { get; set; }
    public string Description { get; set; }
    public string AdminName { get; set; }
    public int NumberParticipants { get; set; }
    public int ScalerId { get; set; }
    public int ClimbingGymId { get; set; }
}