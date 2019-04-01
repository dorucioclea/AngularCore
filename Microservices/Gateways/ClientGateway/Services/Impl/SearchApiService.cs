using AngularCore.Microservices.Gateways.Http;
using ClientGateway.Helpers;
using ClientGateway.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientGateway.Services
{
    public class SearchApiService : ISearchApiService
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
