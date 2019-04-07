using System;

namespace AngularCore.Microservices.Services.Events
{
    public class UserAddedEvent
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; } = "http://localhost:15003/upload/default-profile-pic.png";
    }
}
