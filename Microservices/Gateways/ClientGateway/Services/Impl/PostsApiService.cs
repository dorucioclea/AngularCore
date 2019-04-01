using AngularCore.Microservices.Gateways.Http;
using ClientGateway.Helpers;
using ClientGateway.Models;
using ClientGateway.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientGateway.Services
{
    public class PostsApiService : IPostsApiService
    {
        private readonly IHttpClient _apiClient;
        private readonly ApiConfig _apiConfig;

        public PostsApiService(IHttpClient httpClient, ApiConfig apiConfig)
        {
            _apiClient = httpClient;
            _apiConfig = apiConfig;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var uriString = _apiConfig.PostsApiUrl + "/api/posts";
            return await _apiClient.GetAsync<IEnumerable<Post>>(uriString);
        }

        public async Task<IEnumerable<Post>> GetUserWall(Guid userId)
        {
            var uriString = _apiConfig.PostsApiUrl + "/api/posts/wall/" + userId;
            return await _apiClient.GetAsync<IEnumerable<Post>>(uriString);
        }

        public async Task<Post> GetPost(Guid postId)
        {
            var uriString = _apiConfig.PostsApiUrl + "/api/posts/" + postId;
            return await _apiClient.GetAsync<Post>(uriString);
        }

        public async void AddPost(PostCreate create)
        {
            var uriString = _apiConfig.PostsApiUrl + "/api/posts";
            var response = await _apiClient.PostAsync(uriString, create);
            response.EnsureSuccessStatusCode();
        }

        public async void UpdatePost(Guid postId, PostUpdate update)
        {
            var uriString = _apiConfig.PostsApiUrl + "/api/posts/" + postId;
            var response = await _apiClient.PutAsync(uriString, update);
            response.EnsureSuccessStatusCode();
        }

        public async void DeletePost(Guid postId)
        {
            var uriString = _apiConfig.PostsApiUrl + "/api/posts/" + postId;
            var response = await _apiClient.DeleteAsync(uriString);
            response.EnsureSuccessStatusCode();
        }
    }
}
