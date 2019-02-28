using AngularCore.Data.Models;

namespace AngularCore.Services
{
    public interface IUserService
    {
        void MakeFriends(User user, User friend);
    }
}