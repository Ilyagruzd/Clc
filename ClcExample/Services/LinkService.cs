using ClcExample.Context;
using ClcExample.Interfaces;
using ClcExample.Models.Database;
using ClcExample.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace ClcExample.Services;

public class LinkService: ILinkService
{
    private ClcContext _dbContext;
    
    public LinkService(ClcContext context)
    {
        _dbContext = context;
    }
    public async Task<LinkDto> CreateLink(CreateLinkDto dto)
    {
        var existLink = await CheckExist(dto);
        if (existLink != null)
        {
            return new LinkDto(existLink);
        } 
        
        var path = GenerateRandomString(5);

        while (await _dbContext.Links.AnyAsync(el => el.Path == path))
        {
            path = GenerateRandomString(5);
        }
        
        var link = new Link()
        {
            ExternalLink = dto.ExternalLink,
            Path = path,
        };

        var result = await _dbContext.AddAsync(link);

        await _dbContext.SaveChangesAsync();

        return new LinkDto(result.Entity);
    }

    private async Task<Link?> CheckExist(CreateLinkDto dto)
    {
        var link = await _dbContext.Links.FirstOrDefaultAsync(el => el.ExternalLink == dto.ExternalLink);
        return link;
    }
    
    public async Task<string> GetExternalLink(string path)
    {
        var link = await _dbContext.Links.FirstOrDefaultAsync(el => el.Path == path);
        if (link == null) throw new Exception("Ссылка не найдена");
        return link.ExternalLink;
    }
    
    private string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var randomArray = new char[length];

        for (var i = 0; i < length; i++)
        {
            randomArray[i] = chars[random.Next(chars.Length)];
        }

        return new string(randomArray);
    }
}