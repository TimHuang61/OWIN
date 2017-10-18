using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;
using OWIN_OAuth.Providers;

[assembly: OwinStartup(typeof(OWIN_OAuth.Startup))]

namespace OWIN_OAuth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var option = new OAuthAuthorizationServerOptions();
            // 取得token的網址
            option.TokenEndpointPath = new PathString("/Token");
            // Token過期時間
            option.AccessTokenExpireTimeSpan = TimeSpan.FromDays(2);
            // 是否允許使用Http
            option.AllowInsecureHttp = true;
            // 驗證使用者的Provider可覆寫OAuthAuthorizationServerProvider自定義Provider
            option.Provider = new ApplicationAuthorizationServerProvider();
            // 使用AuthorizationServer
            app.UseOAuthAuthorizationServer(option);
            //使用Bearer驗證
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
