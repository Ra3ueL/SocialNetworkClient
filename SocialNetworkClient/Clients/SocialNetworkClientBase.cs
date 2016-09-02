namespace Epam.SocialNetworkClient.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web;

    using Epam.SocialNetworkClient.ClientsModels;
    using Epam.SocialNetworkClient.Interfaces;

    internal abstract class SocialNetworkClientBase : ISocialNetworkClient
    {
        protected readonly string Token;

        private readonly HttpClient _client;

        protected SocialNetworkClientBase(string token, string baseAddress)
        {
            Token = token;
            _client = new HttpClient() { BaseAddress = new Uri(baseAddress) };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected virtual HttpRequestMessage CreateMessage(HttpMethod method, string relativeUrl, IDictionary<string, string> parameters)
        {
            return new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(_client.BaseAddress + relativeUrl + GetQueryString(parameters)),
            };
        }

        protected string GetQueryString(IDictionary<string, string> parameters)
        {
            if (parameters == null || !parameters.Any())
            {
                return null;
            }
            var queryParams = string.Join("&", parameters.Select(
                    p => $"{HttpUtility.UrlEncode(p.Key)}={HttpUtility.UrlEncode(p.Value)}"));
            return "?" + queryParams;
        }

        protected async Task<TResult> GetAsync<TResult>(
            string relativeUrl, 
            IDictionary<string, string> parameters = null)
        {
            return await SendRequestAsync<TResult>(HttpMethod.Get, relativeUrl, parameters);
        }

        protected async Task<TResult> HandleResponse<TResult>(HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                throw new HttpRequestException(content);
            }

            return await responseMessage.Content.ReadAsAsync<TResult>();
        }

        private async Task<TResult> SendRequestAsync<TResult>(
            HttpMethod method, 
            string relativeUrl, 
            IDictionary<string, string> parameters = null)
        {
            var request = CreateMessage(method, relativeUrl, parameters);
            var response = await _client.SendAsync(request);
            return await HandleResponse<TResult>(response);
        }

        public abstract Task<UserInfo> GetUserInfo();
    }
}