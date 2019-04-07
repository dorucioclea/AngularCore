using System.Collections.Generic;

namespace AngularCore.Microservices.Gateways.Api.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfilePictureUrl { get; set; }
        public List<User> Friends { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}
