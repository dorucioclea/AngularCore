using System.Collections.Generic;
using System.Linq;
using AngularCore.Data.Models;

namespace AngularCore.Repositories
{
    public interface IInMemoryUserRepository
    {
        List<User> GetAllUsers();

        User GetUserById(string id);

        User GetUserByEmail(string email);

        void AddUser(User user);

        void DeleteUser(User user);

        void UpdateUser(User user);
    }
}