namespace AngularCore.Data.ViewModels
{
    public class ImageVM
    {
        public string Id { get; set; }
        public UserVM Author { get; set; }
        public string MediaUrl { get; set; }
        public string Title { get; set; }
        public string CreatedAt { get; set; }
    }
}
