
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NetCore2.MVC.BasicAuthentication.Events
{
    public class ValidateCredentialsContext : ResultContext<BasicAuthenticationOptions>
    {
        public ValidateCredentialsContext(
            HttpContext context,
            AuthenticationScheme scheme,
            BasicAuthenticationOptions options)
            : base(context, scheme, options)
        {
        }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
