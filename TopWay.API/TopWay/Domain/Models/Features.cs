namespace TopWay.API.TopWay.Domain.Models;

public class Features
{
    public int id { get; set; }
    public string Type_climb { get; set; }
    public DateTime Office_hours_start { get; set; }
    public DateTime Office_hours_end { get; set; }
    public int Routes { get; set; }
    public float Max_height { get; set; }
    public string Rock_type { get; set; }
    public string Bolting { get; set; }
    public double price { get; set; }
    
    public ClimbingGym ClimbingGym { get; set; }
    public int ClimbingGymld { get; set; }
    
}