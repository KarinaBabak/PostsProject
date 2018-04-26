using System;
using System.Collections.Generic;

namespace Posts.Entities.Entities
{
    public class Post : BaseEntity, ITextEntity
    {
        public string UserLogin { get; set; }

        public string Content { get; set; }

        public DateTime LastUpdatedDate { get; set; }


        public virtual ICollection<Comment> Comments { get; set; }
    }
}
