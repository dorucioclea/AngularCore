using System.Collections.Generic;
using AngularCore.Data.Models;

namespace AngularCore.Repositories
{
    public interface IPostRepository
    {
        List<Post> GetAllPosts();

        List<Post> GetPostsForUser(User user);

        Post GetPostById(string id);

        Post AddPost(Post post);

        bool DeletePost(Post post);

        bool UpdatePost(string id, Post post);
    }
}