using CRM.Data.Common;
using CRM.Data.Enums;

namespace CRM.Data.Entities;

public class Permission : BaseEntity
{
   public required string Name { get; set; }
   public string? Description { get; set; }
   public string? Module { get; set; }
   public PermissionType Type { get; set; }
   public ICollection<Position>? Positions { get; set; }
}