using System;

namespace AngularCore.Microservices.Services.Events
{
    public class UserUpdatedEvent
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
