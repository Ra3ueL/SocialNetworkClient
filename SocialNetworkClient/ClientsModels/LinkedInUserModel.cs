namespace Epam.SocialNetworkClient.ClientsModels
{
    using Newtonsoft.Json;

    internal class LinkedInUserModel : BaseApiUserModel
    {
        public override string Name => $"{FirstName} {LastName}";

        [JsonProperty("emailAddress")]
        public override string Email { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }
    }
}
