using System.Collections.Generic;
using Posts.Model;
using System.Threading.Tasks;

namespace Posts.Application.Interface
{
    public interface IPostService : IService<Post>
    {
        Task<IEnumerable<Post>> GetAllPosts();
        Task<IEnumerable<Post>> GetPostsByUserLogin(string userLogin);
    }
}
