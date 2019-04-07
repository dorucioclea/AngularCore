using AngularCore.Microservices.Gateways.Api.Models;
using AngularCore.Microservices.Gateways.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngularCore.Microservices.Gateways.Api.Services
{
    public interface IClientPostsApiService
    {
        Task<IEnumerable<Post>> GetPosts();

        Task<IEnumerable<Post>> GetUserWall(Guid userId);

        Task<Post> GetPost(Guid postId);

        void AddPost(PostCreate create);

        void UpdatePost(Guid postId, PostUpdate update);

        void DeletePost(Guid postId);
    }
}
