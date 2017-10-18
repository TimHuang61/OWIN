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
            // 因為覆寫自己想要的provider，所以先通過驗證以方便測試
            context.Validated();

            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // 驗證user logic
            if (context.UserName == "Tim" && context.Password == "123456")
            {
                // 建立身分要求
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                // 新增ClaimTypes
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.OtherPhone, "123456"));
                // 登入
                context.Request.Context.Authentication.SignIn(identity);
                // 驗證通過
                context.Validated(new AuthenticationTicket(identity, null));
            }
            else
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
            }

            return Task.FromResult<object>(null);
        }
    }
}