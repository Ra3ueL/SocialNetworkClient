namespace Epam.SocialNetworkClient.Clients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using global::SocialNetworkClient.Clients;
    using global::SocialNetworkClient.ClientsModels;

    internal class GoogleSocialNetworkClient : SocialNetworkClientBase
    {
        public const string SocialNetworkName = "Google";

        public const string AddressApi = "https://www.googleapis.com/oauth2/v1/";

        private const string AddressGetUserInfo = "userinfo";

        public GoogleSocialNetworkClient(string token)
            : base(token, AddressApi)
        {
        }

        public override async Task<UserInfo> GetUserInfo()
        {
            var parameters = new Dictionary<string, string>
            { 
                { "alt", "json" },
                { "access_token", Token }
            };

            var userInfo = await GetAsync<BaseApiUserModel>(AddressGetUserInfo, parameters);

            return new UserInfo()
            {
                Email = userInfo.Email,
                UserName = userInfo.Name,
                SocialNetworkName = SocialNetworkName
            };
        }
    }
}
