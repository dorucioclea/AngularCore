using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityService.Data
{
    public class UserFriend
    {
        [Key]
        public Guid UserId { get; set; }
        
        [Key]
        public Guid FriendId { get; set; }
    }
}
