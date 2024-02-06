using ClcExample.Context;
using ClcExample.Interfaces;
using ClcExample.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("ClcContext");
builder.Services.AddDbContext<ClcContext>(options => options.UseNpgsql(connection));

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ILinkService, LinkService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "redirect",
    pattern: "{path}",
    defaults: new { controller = "Redirect", action = "RedirectTo" });

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Request.Path = "/Home/Index";
        await next();
    }
    else
    {
        await next();
    }
});

app.Run();
