using AngularCore.Microservices.Gateways.Api.ViewModels;
using System.Threading.Tasks;

namespace AngularCore.Microservices.Gateways.Api.Services
{
    public interface IClientIdentityApiService
    {
        Task<SessionResponse> Register(RegisterForm form);

        Task<SessionResponse> Login(LoginForm form);

        Task<SessionResponse> RenewSession();
    }
}
