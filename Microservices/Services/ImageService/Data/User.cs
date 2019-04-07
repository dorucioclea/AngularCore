using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageService.Data
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePictureUrl { get; set; }

        [NotMapped]
        public string FullName
        {
            get => $"{FirstName} {LastName}";
        }
    }
}
