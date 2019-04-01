using System;

namespace ClientGateway.Models
{
    public class Image : BaseEntity
    {
        public Guid AuthorId { get; set; }
        public string MediaUrl { get; set; }
        public string Title { get; set; }
    }
}
