namespace TopWay.API.TopWay.Domain.Models;

public class Notification
{
    public int Id { get; set; }
    public string Message { get; set; }
    public int ScalerId  { get; set; }
    public Scaler Scaler { get; set; }
}