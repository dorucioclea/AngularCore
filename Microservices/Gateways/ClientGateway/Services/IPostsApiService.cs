using ClientGateway.Models;
using ClientGateway.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientGateway.Services
{
    public interface IPostsApiService
    {
        Task<IEnumerable<Post>> GetPosts();

        Task<IEnumerable<Post>> GetUserWall(Guid userId);

        Task<Post> GetPost(Guid postId);

        void AddPost(PostCreate create);

        void UpdatePost(Guid postId, PostUpdate update);

        void DeletePost(Guid postId);
    }
}
