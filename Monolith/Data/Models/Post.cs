using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngularCore.Data.Models
{
    public class Post : BaseEntity
    {
        public string AuthorId { get; set; }

        [Required]
        public User Author { get; set; }

        public string WallOwnerId { get; set; }

        [Required]
        public User WallOwner { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        [Required]
        public virtual string Content { get; set; }
    }
}