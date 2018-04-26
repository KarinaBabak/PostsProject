using System.Collections.Generic;
using Posts.Application.Interface;
using Posts.Model;
using Posts.Persistence.Interface;
using System.Threading.Tasks;
using System;

namespace Posts.Application.Implementation
{
    public class PostService : BaseTextService<Post>, IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository repository): base(repository)
        {
            this._postRepository = repository;
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _postRepository.GetAll();
        }

        public async Task<IEnumerable<Post>> GetPostsByUserLogin(string userLogin)
        {
            return await _postRepository.GetPostsByUserLogin(userLogin);
        }
    }
}
