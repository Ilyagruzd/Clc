using ClcExample.Models.Database;

namespace ClcExample.Models.DTO;

public record LinkDto
{
    public LinkDto(Link link)
    {
        Path = link.Path;
        ExternalLink = link.ExternalLink;
    }
    public string Path { get; set; }
    
    public string ExternalLink { get; set; }
};