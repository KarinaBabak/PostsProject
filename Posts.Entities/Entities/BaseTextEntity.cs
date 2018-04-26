using System;

namespace Posts.Entities.Entities
{
    public interface ITextEntity
    {
        string Content { get; set; }

        DateTime LastUpdatedDate { get; set; }
    }
}
