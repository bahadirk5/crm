namespace CRM.Business.DTOs;

public class PositionDto
{
   public int Id { get; set; }
   public required string Title { get; set; }
   public string? Description { get; set; }
   public int Level { get; set; }
   public int DepartmentId { get; set; }
   public int? ParentPositionId { get; set; }
}