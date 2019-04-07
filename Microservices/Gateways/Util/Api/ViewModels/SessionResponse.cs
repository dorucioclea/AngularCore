using AngularCore.Microservices.Gateways.Api.Models;

namespace AngularCore.Microservices.Gateways.Api.ViewModels
{
    public class SessionResponse
    {
        public User User { get; set; }

        public string JwtToken { get; set; }

        public string ExpiresIn { get; set; }
    }
}
