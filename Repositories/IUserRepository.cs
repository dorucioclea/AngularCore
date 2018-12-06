using System.Collections.Generic;
using AngularCore.Data.Models;

namespace AngularCore.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();

        User GetUserById(string id);

        User GetUserByEmail(string email);

        User AddUser(User user);

        bool DeleteUser(User user);

        bool UpdateUser(string id, User user);
    }
}