using Authentication.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Authorization.Infrastructure;

namespace Authentication.Policies
{
    public class MustBeFromSameLocationToEditHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, Customer>
    {
        protected override void Handle(AuthorizationContext context,
            OperationAuthorizationRequirement requirement, Customer resource)
        {
            if (requirement == CustomerOperations.Edit)
            {
                if (context.User.HasClaim("Location", resource.Location))
                {
                    context.Succeed(requirement);
                }
            }
        }
    }

}
