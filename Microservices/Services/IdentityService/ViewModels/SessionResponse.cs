using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.ViewModels
{
    public class SessionResponse
    {
        public Guid UserId { get; set; }

        public string JwtToken { get; set; }

        public string ExpiresIn { get; set; }
    }
}
