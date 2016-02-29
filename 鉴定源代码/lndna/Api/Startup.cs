using Owin;
using System.Configuration;
using Thinktecture.IdentityServer.AccessTokenValidation;

namespace Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = ConfigurationManager.AppSettings["OAuthHost"],
                RequiredScopes = new[] { "api1" }
            });
        }
    }
}