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
            CreateMap<Post, PostVM>()
                .ForMember(
                    pvm => pvm.Owner,
                    opt => opt.MapFrom( p => p.User )
                ).ReverseMap();
            CreateMap<User, UserVM>().ReverseMap();
            CreateMap<User, string>().ConvertUsing( u => u.Id );
        }
    }
}