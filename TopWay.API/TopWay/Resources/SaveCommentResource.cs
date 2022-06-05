using System.ComponentModel.DataAnnotations;

namespace TopWay.API.TopWay.Resources;

public class SaveCommentResource
{
    [Required]
    [MaxLength(50)]
    public string Description { get; set; }
    
    [Required]
    public double Score { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
}