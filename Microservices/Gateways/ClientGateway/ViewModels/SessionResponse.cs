using System;

namespace ClientGateway.ViewModels
{
    public class SessionResponse
    {
        public Guid UserId { get; set; }

        public string JwtToken { get; set; }

        public string ExpiresIn { get; set; }
    }
}
