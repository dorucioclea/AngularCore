using System.ComponentModel.DataAnnotations;

namespace AngularCore.Data.Models
{
    public class Comment : BaseEntity
    {
        [Required]
        public User User { get; set; }

        [Required]
        public Post Post { get; set; }

        [Required]
        [MaxLength(256)]
        public string Content { get; set; }
    }
}