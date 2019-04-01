using System;

namespace ClientGateway.Models
{
    public class Post : BaseEntity
    {
        public Guid AuthorId { get; set; }
        public Guid WallOwnerId { get; set; }
        public string Content { get; set; }
    }
}
