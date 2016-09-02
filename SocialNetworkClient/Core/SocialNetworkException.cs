namespace Epam.SocialNetworkClient.Core
{
    using System;

    public class SocialNetworkException : Exception
    {
        public SocialNetworkException(string message)
            : base(message)
        {
        }
    }
}
