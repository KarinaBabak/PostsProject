using System;

namespace Posts.Model.Interface
{
    public interface IText : IEntity
    {
        string UserLogin { get; set; }

        string Content { get; set; }

        DateTime LastUpdatedDate { get; set; }
    }
}
