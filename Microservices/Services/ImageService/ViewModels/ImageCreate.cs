using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageService.ViewModels
{
    public class ImageCreate
    {
        public Guid AuthorId { get; set; }

        public string FileName { get; set; }

        public string Title { get; set; }

        public string ImageBase64 { get; set; }
    }
}
