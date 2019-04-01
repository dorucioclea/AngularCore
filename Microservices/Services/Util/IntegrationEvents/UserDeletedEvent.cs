using System;

namespace AngularCore.Microservices.Services.Events
{
    public class UserDeletedEvent
    {
        public Guid UserId { get; set; }
    }
}
