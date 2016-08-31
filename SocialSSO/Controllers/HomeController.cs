namespace SocialSSOWithoutIdentity.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Models;
    using Shared;

    using SocialNetworkClient.ClientsModels;
    using SocialNetworkClient.Core;

    public class HomeController : Controller
    {
        public async Task<ActionResult> AuthorizeResult(SocialNetworkType authorizeType)
        {
            var authenticateResult = await Request.GetOwinContext().Authentication.AuthenticateAsync("ExternalCookie");

            if (authenticateResult != null)
            {
                IEnumerable<Claim> claims = authenticateResult.Identity.Claims;
                var currentClaim = claims.FirstOrDefault(x => x.Type == authorizeType.ToString());
                if (currentClaim != null)
                {
                    var client = SocialNetworkClientFactory.GetClient(authorizeType, currentClaim.Value);

                    UserInfo userInfo = await client.GetUserInfo();

                    UserDisplayModel displayModel = new UserDisplayModel
                    {
                        SocialNetworkName = userInfo.SocialNetworkName, 
                        Name = userInfo.UserName, 
                        Email = userInfo.Email, 
                        AccessToken = currentClaim.Value
                    };

                    return View(displayModel);
                }
            }

            return View("Index");
        }

        public ActionResult ExternalLogin(string provider)
        {
            var type = Enum.Parse(typeof(SocialNetworkType), provider);

            return new ChallengeResult(provider, Url.Action("AuthorizeResult", "Home", new { authorizeType = type }));
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}