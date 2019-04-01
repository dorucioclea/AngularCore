using System;
using System.Collections.Generic;

namespace IdentityService.Data
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public List<UserFriend> Friends { get; set; }
    }
}
