using System.ComponentModel.DataAnnotations;

namespace AngularCore.Data.Models
{
    public class Image : Post
    {
        [Required]
        public string MediaUrl { get; set; }

        public override string Content { get; set; }
    }
}