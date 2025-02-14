using Microsoft.AspNetCore.Authorization;

namespace CRM.Web.Authorization;

public static class PolicyRequirements
{
  public static void AddPolicies(AuthorizationOptions options)
  {
    options.AddPolicy(PolicyNames.RequireDirectorLevel, policy =>
        policy.RequireAssertion(context =>
        {
          var positionLevel = int.Parse(context.User.Claims
                  .FirstOrDefault(c => c.Type == "PositionLevel")?.Value ?? "0");
          return positionLevel <= 2;
        }));

    options.AddPolicy(PolicyNames.RequireITDepartment, policy =>
        policy.RequireAssertion(context =>
        {
          var departmentId = int.Parse(context.User.Claims
                  .FirstOrDefault(c => c.Type == "DepartmentId")?.Value ?? "0");
          return departmentId == 1;
        }));
  }
}