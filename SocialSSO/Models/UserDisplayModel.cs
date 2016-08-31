namespace SocialSSOWithoutIdentity.Models
{
    public class UserDisplayModel
    {
        public string AccessToken { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string SocialNetworkName { get; set; }
    }
}