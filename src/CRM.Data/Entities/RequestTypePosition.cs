using CRM.Data.Common;

namespace CRM.Data.Entities;
public class RequestTypePosition : BaseEntity
{
  public int RequestTypeId { get; set; }
  public RequestType? RequestType { get; set; }
  public int PositionId { get; set; }
  public Position? Position { get; set; }
  public int RequiredApprovalLevel { get; set; }
}