﻿using System;

namespace PostService.ViewModels
{
    public class PostCreate
    {
        public Guid AuthorId { get; set; }
        
        public Guid WallOwnerId { get; set; }

        public string Content { get; set; }
    }
}
