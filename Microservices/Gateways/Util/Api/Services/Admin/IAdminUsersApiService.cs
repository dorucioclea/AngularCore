using System;

namespace AngularCore.Microservices.Gateways.Api.Services
{
    public interface IAdminUsersApiService : IClientUsersApiService
    {
        void DeleteUser(Guid userId);
    }
}
