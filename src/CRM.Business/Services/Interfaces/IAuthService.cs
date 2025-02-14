using CRM.Business.DTOs;
using Microsoft.AspNetCore.Http;

namespace CRM.Business.Services.Interfaces;

public interface IAuthService
{
  Task<(bool success, string token)> LoginAsync(string email, string password);
  Task<(bool success, string message)> RegisterAsync(RegisterDto registerDto);
  void SetAuthCookie(HttpContext httpContext, string token);
  void RemoveAuthCookie(HttpContext httpContext);
  bool ValidateToken(string token);
}