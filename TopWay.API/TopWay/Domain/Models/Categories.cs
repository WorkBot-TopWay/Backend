namespace TopWay.API.TopWay.Domain.Models;

public class Categories
{
    public int Id { get; set; }
    public string Name { get; set;}
    public IList<CategoryGyms> CategoryGym { get; set; } = new List<CategoryGyms>();
}