namespace TopWay.API.TopWay.Domain.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set;}
    public IList<CategoryGym> CategoryGym { get; set; } = new List<CategoryGym>();
}