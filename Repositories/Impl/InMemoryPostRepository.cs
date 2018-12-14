using System.Collections.Generic;
using AngularCore.Data.Models;

namespace AngularCore.Repositories.Impl
{
    public class InMemoryPostRepository : IInMemoryPostRepository
    {
        private static List<Post> _posts = new List<Post>();

        public void AddPost(Post post)
        {
            _posts.Add(post);
        }

        public void DeletePost(Post post)
        {
            _posts.Remove(post);
        }

        public void DeletePostById(string postId)
        {
            var post = GetPostById(postId);
            if(post != null)
            {
                DeletePost(post);
            }
        }

        public List<Post> GetAllPosts()
        {
            return _posts;
        }

        public Post GetPostById(string id)
        {
            return _posts.Find( p => p.Id.Equals(id) );
        }

        public List<Post> GetPostsForUser(User user)
        {
            return _posts.FindAll( p => p.User.Id.Equals(user.Id));
        }

        public void UpdatePost(Post post)
        {
            var foundPost = GetPostById(post.Id);
            if(foundPost != null)
            {
                DeletePost(foundPost);
                AddPost(post);
            }
        }
    }
}