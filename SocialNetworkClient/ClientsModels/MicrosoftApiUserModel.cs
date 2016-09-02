namespace Epam.SocialNetworkClient.ClientsModels
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    internal class MicrosoftApiUserModel : BaseApiUserModel
    {
        public override string Email => Emails.FirstOrDefault(x => x.Key == "preferred").Value;

        [JsonProperty("emails")]
        public Dictionary<string, string> Emails { get; set; }
    }
}
