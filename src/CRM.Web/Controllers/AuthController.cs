using CRM.Business.DTOs;
using CRM.Business.Services.Interfaces;
using CRM.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Web.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly IPositionService _positionService;

    public AuthController(IAuthService authService, IPositionService positionService)
    {
        _authService = authService;
        _positionService = positionService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated ?? false)
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var (success, token) = await _authService.LoginAsync(model.Email, model.Password);

        if (!success)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        _authService.SetAuthCookie(HttpContext, token);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        var positions = await _positionService.GetAllPositionsAsync();
        ViewBag.Positions = new SelectList(positions, "Id", "Title");
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var positions = await _positionService.GetAllPositionsAsync();
            ViewBag.Positions = new SelectList(positions, "Id", "Title");
            return View(model);
        }

        var registerDto = new RegisterDto
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Password = model.Password,
            PositionId = model.PositionId
        };

        var (success, message) = await _authService.RegisterAsync(registerDto);

        if (!success)
        {
            ModelState.AddModelError(string.Empty, message);
            var positions = await _positionService.GetAllPositionsAsync();
            ViewBag.Positions = new SelectList(positions, "Id", "Title");
            return View(model);
        }

        var (loginSuccess, token) = await _authService.LoginAsync(model.Email, model.Password);

        if (loginSuccess)
        {
            _authService.SetAuthCookie(HttpContext, token);
            return RedirectToAction("Index", "Home");
        }

        TempData["SuccessMessage"] = "Registration successful. Please login.";
        return RedirectToAction(nameof(Login));
    }

    [HttpPost]
    public IActionResult Logout()
    {
        _authService.RemoveAuthCookie(HttpContext);
        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}