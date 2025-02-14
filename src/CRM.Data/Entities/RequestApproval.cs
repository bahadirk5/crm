using CRM.Data.Common;
using CRM.Data.Enums;

namespace CRM.Data.Entities;

public class RequestApproval : BaseEntity
{
   public int RequestId { get; set; }
   public Request? Request { get; set; }
   public int ApproverId { get; set; }
   public Employee? Approver { get; set; }
   public int Level { get; set; }
   public ApprovalStatus Status { get; set; }
   public string? Comment { get; set; }
}