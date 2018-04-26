using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using AutoMapper;
using Posts.Persistence.Interface;
using CommentModel = Posts.Model.Comment;
using CommentEntity = Posts.Entities.Entities.Comment;
using System.Threading.Tasks;

namespace Posts.Persistence.Implementation
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DbContext _dbContext;

        public CommentRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Add(CommentModel entity)
        {
            var comment = Mapper.Map<CommentModel, CommentEntity>(entity);
            var resultEntity = _dbContext.Set<CommentEntity>().Add(comment);
            await _dbContext.SaveChangesAsync();
            return resultEntity.Id;
        }

        public async Task Delete(CommentModel entity)
        {
            var comment = _dbContext.Set<CommentEntity>().Where(c => c.Id == entity.Id).FirstOrDefault();
            if (comment != null)
            {
                _dbContext.Set<CommentEntity>().Remove(comment);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<CommentModel> GetById(int commentId)
        {
            return await _dbContext.Set<CommentEntity>()
                .Where(c => c.Id == commentId)
                .Select(comment => Mapper.Map<CommentEntity, CommentModel>(comment)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CommentModel>> GetByPostId(int postId)
        {
            return await _dbContext.Set<CommentEntity>().Where(a => a.PostId == postId)
                .Select(comment => Mapper.Map<CommentEntity, CommentModel>(comment))
                .ToListAsync();
        }

        public async Task Update(CommentModel entity)
        {
            var comment = _dbContext.Set<CommentEntity>().Where(c => c.Id == entity.Id).FirstOrDefault();

            if (comment != null)
            {
                comment = Mapper.Map<CommentModel, CommentEntity>(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
