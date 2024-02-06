using ClcExample.Models.DTO;

namespace ClcExample.Interfaces;

public interface ILinkService
{
    public Task<LinkDto> CreateLink(CreateLinkDto dto);
    public Task<string> GetExternalLink(string path);
}