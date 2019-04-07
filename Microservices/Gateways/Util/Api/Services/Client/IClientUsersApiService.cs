using AngularCore.Microservices.Gateways.Api.Models;
using AngularCore.Microservices.Gateways.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngularCore.Microservices.Gateways.Api.Services
{
    public interface IClientUsersApiService
    {
        Task<IEnumerable<User>> GetUsers();

        Task<User> GetUser(Guid userId);

        void UpdateUser(Guid userId, UserUpdate update);

        Task<IEnumerable<User>> GetUserFriends(Guid userId);

        void AddUserFriend(Guid userId, AddFriend addFriend);

        void RemoveUserFriend(Guid userId, Guid friendId);
    }
}
