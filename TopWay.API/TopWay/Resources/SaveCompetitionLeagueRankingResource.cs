using System.ComponentModel.DataAnnotations;

namespace TopWay.API.TopWay.Resources;

public class SaveCompetitionLeagueRankingResource
{
    [Required]
    public double Score { get; set; }
}