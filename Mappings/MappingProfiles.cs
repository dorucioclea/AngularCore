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
            CreateMap<User, FriendUserVM>().ReverseMap();
            CreateMap<User, UserVM>().ReverseMap();
                // TODO
                // .ForMember(
                //     dest => dest.Friends,
                //     opt => opt.MapFrom(
                //         src => {
                //             src.Friends.ForEach( f => {
                //                 new FriendUserVM {
                //                     Id = f.Id,
                //                     Name = f.Name,
                //                     Surname = f.Surname,
                //                     Email = f.Email
                //                 };
                //             });
                //         }
                //     )
                // ).ReverseMap();
        }
    }
}