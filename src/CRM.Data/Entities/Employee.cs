using CRM.Data.Common;

namespace CRM.Data.Entities;

public class Employee : BaseEntity
{
   public required string FirstName { get; set; }
   public required string LastName { get; set; }
   public required string Email { get; set; }
   public string? PasswordHash { get; set; }
   public DateTime HireDate { get; set; }
   public bool IsActive { get; set; }

   public int PositionId { get; set; }
   public Position Position { get; set; } = null!;

   public int? ManagerId { get; set; }
   public Employee? Manager { get; set; }
   public ICollection<Employee> DirectReports { get; set; } = new List<Employee>();
}