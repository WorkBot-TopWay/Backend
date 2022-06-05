namespace TopWay.API.TopWay.Domain.Models;

public class Scaler
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string UrlPhoto { get; set; }
    public string Type { get; set; }
    public IList<Notification> Notifications { get; set; } = new List<Notification>();
    public IList<Comment> Comments { get; set; } = new List<Comment>();
    public IList<CompetitionReservationClimber> CompetitionReservationClimbers { get; set; } = new List<CompetitionReservationClimber>();
}