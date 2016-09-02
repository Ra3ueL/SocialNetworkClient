namespace SocialNetworkClient.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Interfaces;

    public static class SocialNetworkClientFactory
    {
        private const string ClientTypePostfix = "SocialNetworkClient";

        private const string NamespaceClients = "SocialNetworkClient.Clients";

        public static ISocialNetworkClient GetClient(SocialNetworkType clientType, string token)
        {
            return FindClient(clientType, token);
        }

        private static ISocialNetworkClient FindClient(SocialNetworkType clientType, string token)
        {
            ISocialNetworkClient client;

            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == NamespaceClients).ToList();
            var type = types.FirstOrDefault(x => x.Name == $"{clientType.ToString()}{ClientTypePostfix}");

            if (type != null)
            {
                client = (ISocialNetworkClient)Activator.CreateInstance(type, token);
            }
            else
            {
                throw new SocialNetworkException("Specific client was not found in namespace SocialNetworkClient.Clients");
            }

            return client;
        } 
    }
}