using System.ComponentModel.DataAnnotations;

namespace TopWay.API.TopWay.Resources;

public class SaveNotificationResource
{
    [Required]
    [MaxLength(100)]
    public string Message { get; set; }
}