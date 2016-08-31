namespace SocialSSOWithoutIdentity
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Facebook;
    using Microsoft.Owin.Security.Google;
    using Microsoft.Owin.Security.MicrosoftAccount;
    using Microsoft.Owin.Security.Twitter;

    using Owin.Security.Providers.GitHub;

    using SocialNetworkClient.Core;

    internal interface IAuthOptionsFactory
    {
        /// <summary>
        /// Gets Google options for authenticate
        /// </summary>
        GoogleOAuth2AuthenticationOptions GetGoogleAuthOptions();

        /// <summary>
        /// Gets Github options for authenticate
        /// </summary>
        GitHubAuthenticationOptions GetGithubAuthOptions();

        /// <summary>
        /// Gets Facebook options for authenticate
        /// </summary>
        FacebookAuthenticationOptions GetFacebookAuthOptions();

        /// <summary>
        /// Gets Microsoft options for authenticate
        /// </summary>
        MicrosoftAccountAuthenticationOptions GetMicrosoftAuthOptions();
    }

    internal class AuthOptionsFactory : IAuthOptionsFactory
    {
        public GoogleOAuth2AuthenticationOptions GetGoogleAuthOptions()
        {
            return new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "519592893343-loof1ifqh8tov0o0hmlun1fnfs01op5l.apps.googleusercontent.com",
                ClientSecret = "MNgvp1Q7dzVuYPJUlvCffT7Y",
                Provider = new GoogleOAuth2AuthenticationProvider
                {
                    OnAuthenticated = context =>
                    {
                        context.Identity.AddClaim(new Claim(SocialNetworkType.Google.ToString(), context.AccessToken));
                        return Task.FromResult(true);
                    }
                },
                Scope =
                {
                    "https://www.googleapis.com/auth/userinfo.email",
                    "https://www.googleapis.com/auth/userinfo.profile"
                }
            };
        }

        public GitHubAuthenticationOptions GetGithubAuthOptions()
        {
            return new GitHubAuthenticationOptions
            {
                ClientId = "c15945872527d60e5596",
                ClientSecret = "1521d53cdc8db9adae48f47d5012818bc169179f",
                Provider = new GitHubAuthenticationProvider
                {
                    OnAuthenticated = context =>
                    {
                        context.Identity.AddClaim(new Claim(SocialNetworkType.GitHub.ToString(), context.AccessToken));

                        return Task.FromResult(true);
                    }
                }
            };
        }

        public FacebookAuthenticationOptions GetFacebookAuthOptions()
        {
            return new FacebookAuthenticationOptions
            {
                AppId = "293758310979761",
                AppSecret = "9408076a1d3a287099d1bc8a1e9ee975",
                Scope = { "email" },
                Provider = new FacebookAuthenticationProvider
                {
                    OnAuthenticated = context =>
                    {
                        context.Identity.AddClaim(new Claim(SocialNetworkType.Facebook.ToString(), context.AccessToken));

                        return Task.FromResult(true);
                    }
                }
            };
        }

        public MicrosoftAccountAuthenticationOptions GetMicrosoftAuthOptions()
        {
            return new MicrosoftAccountAuthenticationOptions
            {
                ClientId = "84efa606-eeb0-4531-bf4a-ab2c7f2a7b6a",
                ClientSecret = "Ea4Stdeh8rhfa2onOA0QCYj",
                Scope = { "wl.emails", "wl.basic" },
                Provider = new MicrosoftAccountAuthenticationProvider
                {
                    OnAuthenticated = context =>
                    {
                        context.Identity.AddClaim(new Claim(SocialNetworkType.Microsoft.ToString(), context.AccessToken));
                        
                        return Task.FromResult(true);
                    }
                }
            };
        }
    }
}