using System;

namespace AngularCore.Microservices.Services.Events
{
    public class ProfilePictureChangedEvent
    {
        public Guid UserId { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}
