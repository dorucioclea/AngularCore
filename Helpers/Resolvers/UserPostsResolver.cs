using System.Collections.Generic;
using System.Linq;
using AngularCore.Data.Models;
using AngularCore.Data.ViewModels;
using AutoMapper;

namespace AngularCore.Helpers.Resolvers
{
    public class UserPostsResolver : IValueResolver<User, DetailedUserVM, List<PostVM>>
    {
        public List<PostVM> Resolve(User srcUser, DetailedUserVM destUserVM, List<PostVM> member, ResolutionContext context){
            var posts = srcUser.Posts.Union(srcUser.WallPosts).ToList();
            return context.Mapper.Map<List<PostVM>>(posts);
        }
    }
}