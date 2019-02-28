namespace AngularCore.Data.ViewModels
{
    public class PostVM
    {
        public string Id { get; set; }
        public UserVM Author { get; set; }
        public UserVM WallOwner { get; set; }
        public string Content { get; set; }
        public string ModifiedAt { get; set; }
        public string CreatedAt { get; set; }
    }
}