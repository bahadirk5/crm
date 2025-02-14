using CRM.Data.Common;

namespace CRM.Data.Entities;

public class RequestType : BaseEntity
{
   public required string Name { get; set; }
   public bool IsActive { get; set; }
   public List<Request>? Requests { get; set; }
   public List<RequestTypePosition>? RequestTypePositions { get; set; }
}