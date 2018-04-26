using System.Collections.Generic;
using System.Threading.Tasks;
using Posts.Model;

namespace Posts.Application.Interface
{
    public interface ICommentService : IService<Comment>
    {
        Task<IEnumerable<Comment>> GetByPostId(int postId);
    }
}
