using System.ComponentModel.DataAnnotations;

namespace TopWay.API.TopWay.Resources;

public class SaveCompetitionReservationClimberResource
{
    [Required]
    [MaxLength(50)]
    public string Status { get; set; }
}