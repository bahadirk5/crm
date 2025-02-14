using CRM.Data.Common;
using CRM.Data.Enums;

namespace CRM.Data.Entities;

public class Request : BaseEntity
{
  public required string Title { get; set; }
  public required string Description { get; set; }
  public RequestStatus Status { get; set; }
  public int RequestTypeId { get; set; }
  public RequestType? RequestType { get; set; }
  public int RequestOwnerId { get; set; }
  public Employee? RequestOwner { get; set; }
  public List<RequestApproval>? Approvals { get; set; }
}