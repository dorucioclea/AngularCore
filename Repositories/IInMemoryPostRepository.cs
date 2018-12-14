using System.Collections.Generic;
using System.Linq;
using AngularCore.Data.Models;

namespace AngularCore.Repositories
{
    public interface IInMemoryPostRepository
    {
        List<Post> GetAllPosts();

        List<Post> GetPostsForUser(User user);

        Post GetPostById(string id);

        void AddPost(Post post);

        void DeletePost(Post post);

        void UpdatePost(Post post);
    }
}