using ClientGateway.ViewModels;
using System.Threading.Tasks;

namespace ClientGateway.Services
{
    public interface IIdentityApiService
    {
        Task<SessionResponse> Register(RegisterForm form);

        Task<SessionResponse> Login(LoginForm form);

        Task<SessionResponse> RenewSession();
    }
}
