namespace Epam.SocialNetworkClient.Interfaces
{
    using System.Threading.Tasks;

    using Epam.SocialNetworkClient.ClientsModels;

    /// <summary>
    /// Client for sign in from Google, Facebook etc.
    /// </summary>
    public interface ISocialNetworkClient
    {
        /// <summary>
        /// Gets information about signed user
        /// </summary>
        Task<UserInfo> GetUserInfo();
    }
}