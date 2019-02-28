using System.ComponentModel.DataAnnotations;

namespace AngularCore.Data.Models
{
    public class Image : BaseEntity
    {
        public string AuthorId { get; set; }

        [Required]
        public User Author { get; set; }

        [Required]
        public string MediaUrl { get; set; }

        public string Title { get; set; }
    }
}