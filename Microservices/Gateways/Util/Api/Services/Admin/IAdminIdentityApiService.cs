using AngularCore.Microservices.Gateways.Api.ViewModels;

namespace AngularCore.Microservices.Gateways.Api.Services
{
    public interface IAdminIdentityApiService : IClientIdentityApiService
    {
        void PromoteToAdmin(ChangeUserPrivileges userChange);

        void DegradeFromAdmin(ChangeUserPrivileges userChange);
    }
}
