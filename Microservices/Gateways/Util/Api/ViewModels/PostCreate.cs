using System;

namespace AngularCore.Microservices.Gateways.Api.ViewModels
{
    public class PostCreate
    {
        public Guid AuthorId { get; set; }
        
        public Guid WallOwnerId { get; set; }

        public string Content { get; set; }
    }
}
