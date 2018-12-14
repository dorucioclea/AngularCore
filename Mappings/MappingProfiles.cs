using System.Linq;
using AngularCore.Data.Models;
using AngularCore.Data.ViewModels;
using AutoMapper;

namespace AngularCore.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Post, PostVM>().ReverseMap();
            CreateMap<UserFriends, FriendUserVM>().ReverseMap();
            CreateMap<User, UserVM>().ReverseMap();
        }
    }
}