using AngularCore.Microservices.Gateways.Http;
using ClientGateway.Helpers;
using ClientGateway.ViewModels;
using System.Threading.Tasks;

namespace ClientGateway.Services
{
    public class IdentityApiService : IIdentityApiService
    {
        private readonly IHttpClient _apiClient;
        private readonly ApiConfig _apiConfig;

        public IdentityApiService(IHttpClient httpClient, ApiConfig apiConfig)
        {
            _apiClient = httpClient;
            _apiConfig = apiConfig;
        }
        public async Task<SessionResponse> Register(RegisterForm form)
        {
            var uriString = _apiConfig.IdentityApiUrl + "/api/identity/register";
            return await _apiClient.PostAsync<SessionResponse, RegisterForm>(uriString, form);
        }

        public async Task<SessionResponse> Login(LoginForm form)
        {
            var uriString = _apiConfig.IdentityApiUrl + "/api/identity/login";
            return await _apiClient.PostAsync<SessionResponse, LoginForm>(uriString, form);
        }

        public async Task<SessionResponse> RenewSession()
        {
            var uriString = _apiConfig.IdentityApiUrl + "/api/identity/renew";
            return await _apiClient.GetAsync<SessionResponse>(uriString);
        }
    }
}
