using AngularCore.Microservices.Gateways.Http;
using AngularCore.Microservices.Gateways.Api.Helpers;
using AngularCore.Microservices.Gateways.Api.Models;
using AngularCore.Microservices.Gateways.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngularCore.Microservices.Gateways.Api.Services
{
    public class ImagesApiService : IAdminImagesApiService
    {
        private readonly IHttpClient _apiClient;
        private readonly ApiConfig _apiConfig;

        public ImagesApiService(IHttpClient httpClient, ApiConfig apiConfig)
        {
            _apiClient = httpClient;
            _apiConfig = apiConfig;
        }

        public async Task<IEnumerable<Image>> GetImages()
        {
            var uriString = _apiConfig.ImagesApiUrl + "/api/images";
            return await _apiClient.GetAsync<IEnumerable<Image>>(uriString);
        }

        public async Task<IEnumerable<Image>> GetUserImages(Guid userId)
        {
            var uriString = _apiConfig.ImagesApiUrl + "/api/images/author/" + userId;
            return await _apiClient.GetAsync<IEnumerable<Image>>(uriString);
        }

        public async Task<Image> GetImage(Guid imageId)
        {
            var uriString = _apiConfig.ImagesApiUrl + "/api/images/" + imageId;
            return await _apiClient.GetAsync<Image>(uriString);
        }

        public async Task<Image> GetProfilePicture(Guid userId)
        {
            var uriString = _apiConfig.ImagesApiUrl + "/api/images/profile/" + userId;
            return await _apiClient.GetAsync<Image>(uriString);
        }

        public async void ChangeProfilePicture(ProfilePictureUpdate update)
        {
            var uriString = _apiConfig.ImagesApiUrl + "/api/images/profile";
            var response = await _apiClient.PostAsync(uriString, update);
            response.EnsureSuccessStatusCode();
        }

        public async void AddImage(ImageCreate create)
        {
            var uriString = _apiConfig.ImagesApiUrl + "/api/images";
            var response = await _apiClient.PostAsync(uriString, create);
            response.EnsureSuccessStatusCode();
        }

        public async void UpdateImage(Guid imageId, ImageUpdate update)
        {
            var uriString = _apiConfig.ImagesApiUrl + "/api/images/" + imageId;
            var response = await _apiClient.PutAsync(uriString, update);
            response.EnsureSuccessStatusCode();
        }

        public async void DeleteImage(Guid imageId)
        {
            var uriString = _apiConfig.ImagesApiUrl + "/api/images/" + imageId;
            var response = await _apiClient.DeleteAsync(uriString);
            response.EnsureSuccessStatusCode();
        }
    }
}
