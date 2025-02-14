using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CRM.Business.DTOs;
using CRM.Business.Services.Interfaces;
using CRM.Data.Entities;
using CRM.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CRM.Business.Services;

public class AuthService : IAuthService
{
  private readonly IEmployeeRepository _employeeRepository;
  private readonly IConfiguration _configuration;

  public AuthService(
      IEmployeeRepository employeeRepository,
      IConfiguration configuration)
  {
    _employeeRepository = employeeRepository;
    _configuration = configuration;
  }

  public async Task<(bool success, string token)> LoginAsync(string email, string password)
  {
    try
    {
      var employee = await _employeeRepository.GetByEmailWithPositionAsync(email);
      if (employee == null)
        return (false, "Invalid email or password.");

      if (!VerifyPassword(password, employee.PasswordHash!))
        return (false, "Invalid email or password.");

      var token = GenerateJwtToken(employee);
      return (true, token);
    }
    catch (Exception)
    {
      return (false, "An error occurred during login.");
    }
  }

  public async Task<(bool success, string message)> RegisterAsync(RegisterDto registerDto)
  {
    if (await _employeeRepository.EmailExistsAsync(registerDto.Email))
      return (false, "This email address is already in use.");

    var employee = new Employee
    {
      FirstName = registerDto.FirstName,
      LastName = registerDto.LastName,
      Email = registerDto.Email,
      PositionId = registerDto.PositionId,
      PasswordHash = HashPassword(registerDto.Password)
    };

    await _employeeRepository.AddAsync(employee);
    return (true, "Registration successful.");
  }

  private static string HashPassword(string password)
  {
    return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
  }

  private static bool VerifyPassword(string password, string hashedPassword)
  {
    return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
  }

  public void SetAuthCookie(HttpContext httpContext, string token)
  {
    httpContext.Response.Cookies.Append("JWT", token, new CookieOptions
    {
      HttpOnly = true,
      Secure = true,
      SameSite = SameSiteMode.Strict,
      Expires = DateTime.UtcNow.AddHours(12)
    });
  }

  public void RemoveAuthCookie(HttpContext httpContext)
  {
    httpContext.Response.Cookies.Delete("JWT");
  }

  public bool ValidateToken(string token)
  {
    try
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

      tokenHandler.ValidateToken(token, new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
      }, out _);

      return true;
    }
    catch
    {
      return false;
    }
  }

  private string GenerateJwtToken(Employee employee)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(new[]
        {
            new Claim("Id", employee.Id.ToString()),
            new Claim("Email", employee.Email),
            new Claim("PositionId", employee.PositionId.ToString()),
            new Claim("PositionLevel", employee.Position?.Level.ToString() ?? "0"),
            new Claim("DepartmentId", employee.Position?.DepartmentId.ToString() ?? "0"),
            new Claim("FullName", $"{employee.FirstName} {employee.LastName}")
        }),
      Expires = DateTime.UtcNow.AddHours(12),
      SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }
}
