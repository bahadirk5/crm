using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRM.Web.Models;

namespace CRM.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (User.Identity?.IsAuthenticated ?? false)
        {
            ViewBag.UserInfo = new
            {
                FullName = User.Claims.FirstOrDefault(c => c.Type == "FullName")?.Value,
                Email = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value,
                PositionId = User.Claims.FirstOrDefault(c => c.Type == "PositionId")?.Value,
                PositionLevel = User.Claims.FirstOrDefault(c => c.Type == "PositionLevel")?.Value,
                DepartmentId = User.Claims.FirstOrDefault(c => c.Type == "DepartmentId")?.Value
            };
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
