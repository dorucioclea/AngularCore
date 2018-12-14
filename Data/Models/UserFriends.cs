using System;
using System.ComponentModel.DataAnnotations;

namespace AngularCore.Data.Models
{
    public class UserFriends : IEntityDate
    {
        [Key]
        public string UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Key]
        public string FriendId { get; set; }

        [Required]
        public User Friend { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public UserFriends(){}

        public UserFriends(User user, User friend)
        {
            User = user;
            UserId = user.Id;
            Friend = friend;
            FriendId = friend.Id;
        }
    }
}