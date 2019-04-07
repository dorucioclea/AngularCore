namespace ImageService.Data
{
    public class Image : BaseEntity
    {
        public User Author { get; set; }
        
        public string MediaUrl { get; set; }

        public bool IsProfilePicture { get; set; }

        public string Title { get; set; }
    }
}
