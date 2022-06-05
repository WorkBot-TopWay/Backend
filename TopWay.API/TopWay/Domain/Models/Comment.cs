namespace TopWay.API.TopWay.Domain.Models;

public class Comment
{
    public int Id { get; set; }
    public string Description { get; set; }
    public double Score { get; set; }
    public DateTime Date { get; set; }
    public ClimbingGym ClimbingGym { get; set; }
    public int ClimbingGymId { get; set; }
    public Scaler Scaler { get; set; }
    public int ScalerId { get; set; }
}