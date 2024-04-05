using Microsoft.AspNetCore.Authorization;

namespace PomocKolezenska.Authorization.Requirements;

public class AnonymousRequirement : AuthorizationHandler<AnonymousRequirement>, IAuthorizationRequirement
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AnonymousRequirement requirement)
    {
        if (context.User.Claims.Any())
            context.Fail();
        else
            context.Succeed(requirement);
        
        return Task.CompletedTask;
    }
}
