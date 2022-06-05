using System.ComponentModel.DataAnnotations;

namespace TopWay.API.TopWay.Resources;

public class SaveCompetitionGymRankingResource
{
    [Required]
    public double Score { get; set; }
}