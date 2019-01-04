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
            CreateMap<User, UserVM>().ReverseMap();
            CreateMap<User, string>().ConvertUsing( u => u.Id );
            CreateMap<User, DetailedUserVM>().ForMember(
                du => du.Friends,
                opt => opt.MapFrom( src => src.Friends)
            ).ReverseMap();
        }
    }
}