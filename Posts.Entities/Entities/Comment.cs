using System;

namespace Posts.Entities.Entities
{
    public class Comment : BaseEntity, ITextEntity
    {
        public int PostId { get; set; }

        public string UserLogin { get; set; }

        public string Content { get; set; }

        public DateTime LastUpdatedDate { get; set; }
        
        public virtual Post Post { get; set; }
    }
}
