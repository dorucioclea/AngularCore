using System;

namespace AngularCore.Microservices.Gateways.Api.Models
{
    public class Post : BaseEntity
    {
        public User Author { get; set; }
        public User WallOwner { get; set; }
        public string Content { get; set; }
    }
}
