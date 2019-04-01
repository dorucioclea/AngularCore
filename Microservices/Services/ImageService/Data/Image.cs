using System;

namespace ImageService.Data
{
    public class Image : BaseEntity
    {
        public Guid AuthorId { get; set; }
        
        public string MediaUrl { get; set; }

        public bool IsProfilePicture { get; set; }

        public string Title { get; set; }
    }
}
