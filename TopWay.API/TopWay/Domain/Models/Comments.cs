using TopWay.API.Security.Domain.Models;

namespace TopWay.API.TopWay.Domain.Models;

public class Comments
{
    public int Id { get; set; }
    public string Description { get; set; }
    public double Score { get; set; }
    public DateTime Date { get; set; }
    public ClimbingGyms ClimbingGyms { get; set; }
    public int ClimbingGymId { get; set; }
    public Scaler Scaler { get; set; }
    public int ScalerId { get; set; }
}