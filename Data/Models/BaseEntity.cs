using System;
using System.ComponentModel.DataAnnotations;

namespace AngularCore.Data.Models
{
    public class BaseEntity : IEntityDate
    {
        [Key]
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}