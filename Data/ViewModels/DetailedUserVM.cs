using System.Collections.Generic;

namespace AngularCore.Data.ViewModels
{
    public class DetailedUserVM
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsAdmin { get; set; }
        public ImageVM ProfilePicture { get; set; }
        public List<UserVM> Friends { get; set; }
        public List<PostVM> Posts { get; set; }
        public List<ImageVM> Images { get; set; }
    }
}