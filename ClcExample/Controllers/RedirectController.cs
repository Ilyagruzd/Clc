using ClcExample.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class RedirectController : Controller
{
    private readonly ILinkService _linkService;
    
    public RedirectController(ILinkService linkService)
    {
        _linkService = linkService;
    }
    
    public async Task<IActionResult> RedirectTo(string path)
    {
        try
        {
            var result = await _linkService.GetExternalLink(path);
            
            if (!result.StartsWith("http://") && !result.StartsWith("https://"))
            {
                result = "http://" + result;
            }
            
            return Redirect(result);
        }
        catch (Exception e)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}