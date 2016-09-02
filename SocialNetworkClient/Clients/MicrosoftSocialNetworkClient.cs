namespace Epam.SocialNetworkClient.Clients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using global::SocialNetworkClient.Clients;
    using global::SocialNetworkClient.ClientsModels;

    internal class MicrosoftSocialNetworkClient : SocialNetworkClientBase
    {
        public const string SocialNetworkName = "Microsoft";

        public const string AddressApi = "https://apis.live.net/v5.0/";

        private const string AddressGetUserInfo = "me";

        public MicrosoftSocialNetworkClient(string token)
            : base(token, AddressApi)
        {
        }

        public override async Task<UserInfo> GetUserInfo()
        {
            var parameters = new Dictionary<string, string>() { { "access_token", Token } };
            var userInfo = await GetAsync<MicrosoftApiUserModel>(AddressGetUserInfo, parameters);

            return new UserInfo
            {
                Email = userInfo.Email,
                UserName = userInfo.Name,
                SocialNetworkName = SocialNetworkName
            };
        }
    }
}
