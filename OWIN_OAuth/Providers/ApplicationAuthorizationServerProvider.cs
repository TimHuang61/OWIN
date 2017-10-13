using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace OWIN_OAuth.Providers
{
    public class ApplicationAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (context.UserName == "Tim" && context.Password == "123456")
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.OtherPhone, "123456"));
                context.Request.Context.Authentication.SignIn(identity);
                context.Validated(new AuthenticationTicket(identity, CreateProperties(context.UserName)));
            }
            else
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
            }

            return Task.FromResult<object>(null);
        }

        private AuthenticationProperties CreateProperties(string userName)
        {
            return new AuthenticationProperties(new Dictionary<string, string> { ["userName"] = userName });
        }
    }
}