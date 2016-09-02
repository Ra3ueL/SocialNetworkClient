namespace Epam.SocialNetworkClient.ClientsModels
{
    using Newtonsoft.Json;

    internal class GithubApiUserModel : BaseApiUserModel
    {
        [JsonProperty("login")]
        public override string Name { get; set; }
    }
}