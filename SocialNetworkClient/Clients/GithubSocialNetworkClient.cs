namespace Epam.SocialNetworkClient.Clients
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ClientsModels;

    internal class GitHubSocialNetworkClient : SocialNetworkClientBase
    {
        public const string SocialNetworkName = "GitHub";

        public const string AddressApi = "https://api.github.com";

        private const string AddressGetUserName = "user";

        private const string AddressGetUserEmail = "user/emails";

        private const string UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0";

        public GitHubSocialNetworkClient(string token)
            : base(token, AddressApi)
        {
        }

        public override async Task<UserInfo> GetUserInfo()
        {
            return new UserInfo
            {
                Email = await GetUserEmail(),
                UserName = await GetUserName(),
                SocialNetworkName = SocialNetworkName
            };
        }

        protected override HttpRequestMessage CreateMessage(HttpMethod method, string relativeUrl, IDictionary<string, string> parameters)
        {
            var requestMessage = base.CreateMessage(method, relativeUrl, parameters);
            requestMessage.Headers.Add("User-Agent", UserAgent);

            return requestMessage;
        }

        private async Task<string> GetUserName()
        {
            var parameters = new Dictionary<string, string> { { "access_token", Token } };
            var result = await GetAsync<GithubApiUserModel>(AddressGetUserName, parameters);

            return result.Name;
        }

        private async Task<string> GetUserEmail()
        {
            var parameters = new Dictionary<string, string> { { "access_token", Token } };
            var result = await GetAsync<List<GithubApiUserModel>>(AddressGetUserEmail, parameters);

            return result.First().Email;
        }
    }
}