using ClientGateway.Models;
using ClientGateway.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientGateway.Services
{
    public interface IUsersApiService
    {
        Task<IEnumerable<User>> GetUsers();

        Task<User> GetUser(Guid userId);

        void UpdateUser(Guid userId, UserUpdate update);

        Task<IEnumerable<User>> GetUserFriends(Guid userId);

        void AddUserFriend(Guid userId, AddFriend addFriend);

        void RemoveUserFriend(Guid userId, Guid friendId);
    }
}
