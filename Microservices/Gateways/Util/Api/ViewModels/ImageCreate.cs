using System;

namespace AngularCore.Microservices.Gateways.Api.ViewModels
{
    public class ImageCreate
    {
        public Guid AuthorId { get; set; }

        public string FileName { get; set; }

        public string Title { get; set; }

        public string ImageBase64 { get; set; }
    }
}
