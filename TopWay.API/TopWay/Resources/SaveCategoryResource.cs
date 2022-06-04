using System.ComponentModel.DataAnnotations;

namespace TopWay.API.TopWay.Resources;

public class SaveCategoryResource
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set;}
}