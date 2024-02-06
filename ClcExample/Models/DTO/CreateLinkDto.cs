using System.ComponentModel.DataAnnotations;

namespace ClcExample.Models.DTO;

public record CreateLinkDto
{
    [Required]
    public string ExternalLink { get; set; } 
};