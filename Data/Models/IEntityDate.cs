using System;

namespace AngularCore.Data.Models
{
    public interface IEntityDate
    {
        DateTime CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
    }
}