using System.Collections.Generic;
using Posts.Application.Interface;
using Posts.Model;
using Posts.Persistence.Interface;
using System.Threading.Tasks;

namespace Posts.Application.Implementation
{
    public class CommentService : BaseTextService<Comment>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository repository) : base(repository)
        {
            this._commentRepository = repository;
        }

        public async Task<IEnumerable<Comment>> GetByPostId(int postId)
        {
            return await _commentRepository.GetByPostId(postId);
        }
    }
}
