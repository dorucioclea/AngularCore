using AngularCore.Microservices.Gateways.Http;
using AngularCore.Microservices.Gateways.Api.Helpers;
using AngularCore.Microservices.Gateways.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngularCore.Microservices.Gateways.Api.ViewModels;

namespace AngularCore.Microservices.Gateways.Api.Services
{
    public class UsersApiService : IAdminUsersApiService
    {
        private readonly IHttpClient _apiClient;
        private readonly ApiConfig _apiConfig;

        public UsersApiService(IHttpClient httpClient, ApiConfig apiConfig)
        {
            _apiClient = httpClient;
            _apiConfig = apiConfig;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var uriString = _apiConfig.IdentityApiUrl + "/api/users";
            return await _apiClient.GetAsync<IEnumerable<User>>(uriString);
        }

        public async Task<User> GetUser(Guid userId)
        {
            var uriString = _apiConfig.IdentityApiUrl + "/api/users/" + userId;
            return await _apiClient.GetAsync<User>(uriString);
        }

        public async void UpdateUser(Guid userId, UserUpdate update)
        {
            var uriString = _apiConfig.IdentityApiUrl + "/api/users/" + userId;
            var response = await _apiClient.PutAsync(uriString, update);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<User>> GetUserFriends(Guid userId)
        {
            var uriString = _apiConfig.IdentityApiUrl + "/api/users/" + userId + "/friends";
            return await _apiClient.GetAsync<IEnumerable<User>>(uriString);
        }

        public async void DeleteUser(Guid userId)
        {
            var uriString = _apiConfig.IdentityApiUrl + "/api/users/" + userId;
            var response = await _apiClient.DeleteAsync(uriString);
            response.EnsureSuccessStatusCode();
        }

        public async void AddUserFriend(Guid userId, AddFriend addFriend)
        {
            var uriString = _apiConfig.IdentityApiUrl + "/api/users/" + userId + "/friends";
            var response = await _apiClient.PostAsync(uriString, addFriend);
            response.EnsureSuccessStatusCode();
        }

        public async void RemoveUserFriend(Guid userId, Guid friendId)
        {
            var uriString = _apiConfig.IdentityApiUrl + "/api/users/" + userId + "/friends/" + friendId;
            var response = await _apiClient.DeleteAsync(uriString);
            response.EnsureSuccessStatusCode();
        }
    }
}
