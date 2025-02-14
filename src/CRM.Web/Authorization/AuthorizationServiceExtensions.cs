namespace CRM.Web.Authorization;

public static class AuthorizationServiceExtensions
{
    public static IServiceCollection AddApplicationAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(PolicyRequirements.AddPolicies);
        return services;
    }
}