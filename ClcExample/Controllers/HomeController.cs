using ClcExample.Interfaces;
using ClcExample.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ClcExample.Controllers;

public class HomeController : Controller
{
    private readonly ILinkService _linkService;
    private static IHttpContextAccessor _httpContextAccessor;

    public HomeController(
        ILinkService linkService,
        IHttpContextAccessor httpContextAccessor)
    {
        _linkService = linkService;
        _httpContextAccessor = httpContextAccessor;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Index(CreateLinkDto dto)
    {
        try
        {
            var result = await _linkService.CreateLink(dto);

            var host = _httpContextAccessor.HttpContext.Request.Host;
            ViewData["Path"] = $"{host.Host}:{host.Port}/{result.Path}";
            ViewData["ExternalLink"] = $"{result.ExternalLink}";
            ViewData["ExternalLinkHref"] = $"http://{result.ExternalLink}";
            return View("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
