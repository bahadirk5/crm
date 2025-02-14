namespace CRM.Business.DTOs;

public class RegisterDto
{
  public required string FirstName { get; set; }
  public required string LastName { get; set; }
  public required string Email { get; set; }
  public required string Password { get; set; }
  public required int PositionId { get; set; }
}
