namespace CRM.Business.DTOs;

public class EmployeeDto
{
   public int Id { get; set; }
   public required string FirstName { get; set; }
   public required string LastName { get; set; }
   public required string Email { get; set; }
   public int PositionId { get; set; }
   public PositionDto? Position { get; set; }
   public int? ManagerId { get; set; }
   public EmployeeDto? Manager { get; set; }
   public bool IsActive { get; set; }
}