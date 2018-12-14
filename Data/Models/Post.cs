using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngularCore.Data.Models
{
    public class Post : BaseEntity
    {
        [Required]
        public User User { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        [Required]
        public virtual string Content { get; set; }
    }
}