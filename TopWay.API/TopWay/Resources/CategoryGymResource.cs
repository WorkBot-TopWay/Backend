namespace TopWay.API.TopWay.Resources;

public class CategoryGymResource
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int ClimbingGymId { get; set; }
    public string ClimbingGymName { get; set; }
}