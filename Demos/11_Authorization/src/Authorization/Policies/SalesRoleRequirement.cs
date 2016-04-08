using Microsoft.AspNet.Authorization;

namespace Authentication.Policies
{
    public class SalesRoleRequirement : 
        AuthorizationHandler<SalesRoleRequirement>, IAuthorizationRequirement
    {
        protected override void Handle(AuthorizationContext context, 
            SalesRoleRequirement requirement)
        {
            if (context.User.HasClaim("role", "Sales"))
            {
                context.Succeed(requirement);
            }
        }
    }

}
