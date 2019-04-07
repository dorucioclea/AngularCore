namespace AngularCore.Microservices.Gateways.Api.Models
{
    public class Image : BaseEntity
    {
        public User Author { get; set; }
        public string MediaUrl { get; set; }
        public string Title { get; set; }
    }
}
