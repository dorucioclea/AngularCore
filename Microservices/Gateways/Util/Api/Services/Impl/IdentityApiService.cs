using AngularCore.Microservices.Gateways.Http;
using AngularCore.Microservices.Gateways.Api.Helpers;
using AngularCore.Microservices.Gateways.Api.ViewModels;
using System.Threading.Tasks;

namespace AngularCore.Microservices.Gateways.Api.Services
{
    public class IdentityApiService : IAdminIdentityApiService
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

        public async void PromoteToAdmin(ChangeUserPrivileges userChange)
        {
            var uriString = _apiConfig.IdentityApiUrl + "/api/identity/promote";
            var response = await _apiClient.PostAsync(uriString, userChange);
        }

        public async void DegradeFromAdmin(ChangeUserPrivileges userChange)
        {
            var uriString = _apiConfig.IdentityApiUrl + "/api/identity/degrade";
            var response = await _apiClient.PostAsync(uriString, userChange);
        }
    }
}
