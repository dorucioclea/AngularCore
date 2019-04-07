using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AngularCore.Microservices.Gateways.Http
{
    public class StandardHttpClient : IHttpClient
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private static readonly HttpClient Client = new HttpClient();

        public StandardHttpClient(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
            => await RequestAsync(HttpMethod.Get, uri);

        public async Task<T> GetAsync<T>(string uri)
        {
            var responseMessage = await RequestAsync(HttpMethod.Get, uri);
            var dataString = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(dataString);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri)
            => await RequestAsync(HttpMethod.Delete, uri);

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T item)
            => await RequestAsync(HttpMethod.Post, uri, item);

        public async Task<TR> PostAsync<TR, TI>(string uri, TI item)
            => await RequestAsync<TR,TI>(HttpMethod.Post, uri, item);

        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T item)
            => await RequestAsync(HttpMethod.Put, uri, item);

        public async Task<TR> PutAsync<TR, TI>(string uri, TI item)
            => await RequestAsync<TR, TI>(HttpMethod.Put, uri, item);

        protected async Task<TR> RequestAsync<TR, TI>(HttpMethod httpMethod, string uri, TI item)
        {
            try
            {
                var responseMessage = await RequestAsync(httpMethod, uri, item);
                var dataString = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TR>(dataString);
            } catch (Exception ex)
            {
                Console.WriteLine("Exception occurred:");
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        protected async Task<HttpResponseMessage> RequestAsync<T>(HttpMethod httpMethod, string uri, T item)
        {
            var requestMessage = new HttpRequestMessage(httpMethod, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json")
            };
            return await SendRequestAsync(requestMessage);
        }

        protected async Task<HttpResponseMessage> RequestAsync(HttpMethod httpMethod, string uri)
        {
            var requestMessage = new HttpRequestMessage(httpMethod, uri);
            return await SendRequestAsync(requestMessage);
        }

        protected async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage requestMessage)
        {
            AddAuthHeader(requestMessage);
            var response = await Client.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return response;
        }

        private void AddAuthHeader(HttpRequestMessage request)
        {
            _contextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authHeader);
            var authToken = authHeader.FirstOrDefault();
            if (!String.IsNullOrEmpty(authToken))
            {
                request.Headers.Add("Authorization", authToken);
            }
        }
    }
}
