using System;
using System.Collections.Generic;
using Posts.Model;
using System.Threading.Tasks;

namespace Posts.Application.Interface
{
    public interface ICommentService : IService<Comment>
    {
        Task<IEnumerable<Comment>> GetByPostId(int postId);
    }
}
