using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace NetCore2.MVC.BasicAuthentication.Authentication
{
    public class AlwaysFailRequirement :
         AuthorizationHandler<AlwaysFailRequirement>,
         IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AlwaysFailRequirement requirement)
        {
            context.Fail();
            return Task.FromResult(0);
        }
    }
}
