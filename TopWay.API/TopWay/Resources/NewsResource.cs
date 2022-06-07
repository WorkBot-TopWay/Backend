namespace TopWay.API.TopWay.Resources;

public class NewsResource
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string UrlImage { get; set; }
    public int ClimbingGymsId { get; set; }
}