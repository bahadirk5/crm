using CRM.Data.Common;

namespace CRM.Data.Entities;

public class Position : BaseEntity
{
   public required string Title { get; set; }
   public string? Description { get; set; }
   public int Level { get; set; }

   public int DepartmentId { get; set; }
   public Department Department { get; set; } = null!;

   public int? ParentPositionId { get; set; }
   public Position? ParentPosition { get; set; }

   public ICollection<Employee> Employees { get; set; } = new List<Employee>();
   public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
   public List<RequestTypePosition> RequestTypePositions { get; set; } = new List<RequestTypePosition>();
}