namespace SocialNetworkClient.Clients
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using ClientsModels;

    internal class LinkedInSocialNetworkClient : SocialNetworkClientBase
    {
        public const string SocialNetworkName = "LinkedIn";

        public const string AddressApi = "https://api.linkedin.com/v1/";

        private const string AddressGetUserInfo = "people/~:(firstName,lastName,email-address)";

        public LinkedInSocialNetworkClient(string token)
            : base(token, AddressApi)
        {
        }

        public override async Task<UserInfo> GetUserInfo()
        {
            var parameters = new Dictionary<string, string>
            {
                { "oauth2_access_token", Token },
                { "format", "json" }
            };

            var userInfo = await GetAsync<LinkedInUserModel>(AddressGetUserInfo, parameters);

            return new UserInfo
            {
                Email = userInfo.Email,
                UserName = userInfo.Name,
                SocialNetworkName = SocialNetworkName
            };
        }
    }
}
