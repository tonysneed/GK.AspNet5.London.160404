using Microsoft.AspNet.Authorization.Infrastructure;

namespace Authentication.Policies
{
    public static class CustomerOperations
    {
        public const string _Edit = "EditCustomer";

        public static readonly OperationAuthorizationRequirement Edit =
            new OperationAuthorizationRequirement { Name = _Edit };
    }

}
