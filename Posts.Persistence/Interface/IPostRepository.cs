using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Posts.Model;

namespace Posts.Persistence.Interface
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetAll();
        Task<IEnumerable<Post>> GetPostsByUserLogin(string userLogin);
    }
}
