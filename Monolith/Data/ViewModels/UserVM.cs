namespace AngularCore.Data.ViewModels
{
    public class UserVM
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public AvatarVM ProfilePicture { get; set; }
        public string Surname { get; set; }
    }
}