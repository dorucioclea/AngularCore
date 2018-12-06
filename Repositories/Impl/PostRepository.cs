using System.Collections.Generic;
using AngularCore.Data.Models;

namespace AngularCore.Repositories.Impl
{
    public class PostRepository : IPostRepository
    {
        private static List<Post> _posts = new List<Post>();

        public Post AddPost(Post post)
        {
            _posts.Add(post);
            return post;
        }

        public bool DeletePost(Post post)
        {
            return _posts.Remove(post);
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
            return _posts.FindAll( p => p.Owner.Id.Equals(user.Id));
        }

        public bool UpdatePost(string id, Post post)
        {
            var foundPost = GetPostById(id);
            if(foundPost != null && id.Equals(post.Id))
            {
                if ( !DeletePost(foundPost) ) return false;
                AddPost(post);
                return true;
            }
            return false;
        }
    }
}