namespace TopWay.API.TopWay.Domain.Models;

public class ClimbingGyms
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string LogoUrl { get; set; }
    public string type { get; set; }
    
    public IList<CategoryGyms> CategoryGyms { get; set; } = new List<CategoryGyms>();
    public IList<Images> Images { get; set; } = new List<Images>();
    public IList<CompetitionGyms> CompetitionGyms { get; set; } = new List<CompetitionGyms>();
    public IList<Comments> Comments { get; set; } = new List<Comments>();
    public IList<League> Leagues { get; set; } = new List<League>();
    public IList<ClimberLeagues> ClimbersLeagues { get; set; } = new List<ClimberLeagues>();
    public IList<Favorite> Favorites { get; set; } = new List<Favorite>();
    public Features Features { get; set; }
    public int FeaturesId { get; set; }
    
    public IList<News> News { get; set; } = new List<News>();
}