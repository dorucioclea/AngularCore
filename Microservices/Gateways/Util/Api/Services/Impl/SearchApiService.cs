using AngularCore.Microservices.Gateways.Http;
using AngularCore.Microservices.Gateways.Api.Helpers;
using AngularCore.Microservices.Gateways.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngularCore.Microservices.Gateways.Api.Services
{
    public class SearchApiService : IAdminSearchApiService
    {
        private readonly IHttpClient _apiClient;
        private readonly ApiConfig _apiConfig;

        public SearchApiService(IHttpClient httpClient, ApiConfig apiConfig)
        {
            _apiClient = httpClient;
            _apiConfig = apiConfig;
        }

        public async Task<IEnumerable<User>> SearchUsers(string phrase)
        {
            var uriString = _apiConfig.SearchApiUrl + "/api/search/" + phrase;
            return await _apiClient.GetAsync<IEnumerable<User>>(uriString);
        }
    }
}
