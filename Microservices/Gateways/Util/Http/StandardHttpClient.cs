using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AngularCore.Microservices.Gateways.Http
{
    public class StandardHttpClient : IHttpClient
    {
        private static readonly HttpClient Client = new HttpClient();
        

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
            var responseMessage = await RequestAsync(httpMethod, uri, item);
            var dataString = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TR>(dataString);
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
            var response = await Client.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return response;
        }
    }
}
