namespace SocialSSOWithoutIdentity
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.Facebook;
    using Microsoft.Owin.Security.Google;
    using Microsoft.Owin.Security.Twitter;
    using Owin;
    using Owin.Security.Providers.GitHub;
    using Owin.Security.Providers.LinkedIn;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType("ExternalCookie");
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ExternalCookie",
                AuthenticationMode = AuthenticationMode.Passive,
                CookieName = ".AspNet.ExternalCookie",
                ExpireTimeSpan = TimeSpan.FromMinutes(5),
            });
            IAuthOptionsFactory authOptions = new AuthOptionsFactory();
            app.UseMicrosoftAccountAuthentication(authOptions.GetMicrosoftAuthOptions());
            app.UseGoogleAuthentication(authOptions.GetGoogleAuthOptions());
            app.UseGitHubAuthentication(authOptions.GetGithubAuthOptions());
            app.UseFacebookAuthentication(authOptions.GetFacebookAuthOptions());
            app.UseLinkedInAuthentication(authOptions.GetLinkedinAuthOptions());
        }

        protected class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
            }

            public string LoginProvider { get; set; }

            public string RedirectUri { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
    }
}
