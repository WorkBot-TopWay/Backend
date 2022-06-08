namespace TopWay.API.TopWay.Resources;

public class CommentResource
{
    public int Id { get; set; }
    public string Description { get; set; }
    public double Score { get; set; }
    public int ScalerId { get; set; }
    public DateTime Date { get; set; }
}