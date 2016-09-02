namespace Epam.SocialNetworkClient.ClientsModels
{
    using Newtonsoft.Json;

    internal class BaseApiUserModel
    {
        [JsonProperty("email")]
        public virtual string Email { get; set; }

        [JsonProperty("name")]
        public virtual string Name { get; set; }
    }
}
