using System.ComponentModel.DataAnnotations;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Resources;

public class SaveNewsResource
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }

    [Required]
    [MaxLength(1000)]
    public string UrlImage { get; set; }
}