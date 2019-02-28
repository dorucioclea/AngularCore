using System.Collections.Generic;
using System.Linq;
using AngularCore.Data.Models;

namespace AngularCore.Repositories
{
    public class InMemoryUserRepository : IInMemoryUserRepository
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

        public User GetUserByEmail(string email)
        {
            return _users.Find( u => u.Email.Equals(email));
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public void DeleteUser(User user)
        {
            _users.Remove(user);
        }

        public void UpdateUser(User user)
        {
            var foundUser = GetUserById(user.Id);
            if(foundUser != null)
            {
                DeleteUser(foundUser);
                AddUser(user);
            }
        }
    }
}