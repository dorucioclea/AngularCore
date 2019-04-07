using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostService.Data
{
    public class Post : BaseEntity
    {
        public User Author { get; set; }

        public User WallOwner { get; set; }

        public string Content { get; set; }
    }
}
