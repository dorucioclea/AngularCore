using ClientGateway.Models;
using ClientGateway.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientGateway.Services
{
    public interface IImagesApiService
    {
        Task<IEnumerable<Image>> GetImages();

        Task<IEnumerable<Image>> GetUserImages(Guid userId);

        Task<Image> GetImage(Guid imageId);

        Task<Image> GetProfilePicture(Guid userId);

        void ChangeProfilePicture(ProfilePictureUpdate update);

        void AddImage(ImageCreate create);

        void UpdateImage(Guid imageId, ImageUpdate update);

        void DeleteImage(Guid imageId);
    }
}
