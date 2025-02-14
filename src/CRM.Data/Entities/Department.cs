using CRM.Data.Common;

namespace CRM.Data.Entities;

public class Department : BaseEntity
{
   public required string Name { get; set; }
   public ICollection<Position> Positions { get; set; } = new List<Position>();
}

