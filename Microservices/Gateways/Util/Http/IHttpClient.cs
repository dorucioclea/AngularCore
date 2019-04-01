using System.Net.Http;
using System.Threading.Tasks;

namespace AngularCore.Microservices.Gateways.Http
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string uri);

        Task<T> GetAsync<T>(string uri);
        
        Task<HttpResponseMessage> DeleteAsync(string uri);

        Task<HttpResponseMessage> PostAsync<T>(string uri, T item);

        Task<TR> PostAsync<TR, TI>(string uri, TI item);

        Task<HttpResponseMessage> PutAsync<T>(string uri, T item);

        Task<TR> PutAsync<TR, TI>(string uri, TI item);
    }
}
