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
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            var option = new OAuthAuthorizationServerOptions();
            // 取得token的網址
            option.TokenEndpointPath = new PathString("/Token");
            // token過期時間
            option.AccessTokenExpireTimeSpan = TimeSpan.FromDays(2);
            // 是否允許使用Http
            option.AllowInsecureHttp = true;
            // 驗證使用者的Provider
            option.Provider = new ApplicationAuthorizationServerProvider();

            // 
            app.UseOAuthAuthorizationServer(option);

            //使用 Bearer 驗證
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
