namespace SocialNetworkClient.Clients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ClientsModels;

    internal class FacebookSocialNetworkClient : SocialNetworkClientBase
    {
        public const string SocialNetworkName = "Facebook";

        public const string AddressApi = "https://graph.facebook.com";

        private const string AddressGetUserName = "me";


        public FacebookSocialNetworkClient(string token)
            : base(token, AddressApi)
        {
        }

        public override async Task<UserInfo> GetUserInfo()
        {
            var parameters = new Dictionary<string, string>
            {
                { "access_token", Token },
                { "fields", "name,email" }
            };
            var result = await GetAsync<BaseApiUserModel>(AddressGetUserName, parameters);

            return new UserInfo()
            {
                Email = result.Email,
                UserName = result.Name,
                SocialNetworkName = SocialNetworkName
            };
        }
    }
}
