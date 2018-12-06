using System.Collections.Generic;
using AngularCore.Data.Models;

namespace AngularCore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static List<User> _users = new List<User>();

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public User GetUserById(string id)
        {
            return _users.Find( u => u.Id.Equals(id));
        }

        public User AddUser(User user)
        {
            _users.Add(user);
            return user;
        }

        public bool DeleteUser(User user)
        {
            return _users.Remove(user);
        }

        public bool UpdateUser(string id, User user)
        {
            var foundUser = GetUserById(id);
            if(foundUser != null && id.Equals(user.Id))
            {
                if ( !DeleteUser(foundUser) ) return false;
                AddUser(user);
                return true;
            }
            return false;
        }
    }
}