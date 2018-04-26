using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Data.Entity;
using Posts.Persistence.Interface;
using PostModel = Posts.Model.Post;
using PostEntity = Posts.Entities.Entities.Post;
using CommentEntity = Posts.Entities.Entities.Comment;
using System.Threading.Tasks;

namespace Posts.Persistence.Implementation
{
    public class PostRepository : IPostRepository
    {
        private readonly DbContext _dbContext;

        public PostRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Add(PostModel entity)
        {
            var post = Mapper.Map<PostModel, PostEntity>(entity);
            var resultEntity = _dbContext.Set<PostEntity>().Add(post);
            await _dbContext.SaveChangesAsync();

            return resultEntity.Id;
        }

        public async Task Delete(PostModel entity)
        {
            var post = _dbContext.Set<PostEntity>().Where(c => c.Id == entity.Id).FirstOrDefault();
            if (post != null)
            {
                _dbContext.Set<PostEntity>().Remove(post);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PostModel>> GetAll()
        {
            var posts = await _dbContext.Set<PostEntity>().ToListAsync();
            return Mapper.Map<List<PostEntity>, List<PostModel>>(posts);
        }

        public async Task<PostModel> GetById(int postId)
        {
            return await _dbContext.Set<PostEntity>()
                .Where(p => p.Id == postId)
                .Include(p => p.Comments)
                .Select(post => Mapper.Map<PostEntity, PostModel>(post)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PostModel>> GetPostsByUserLogin(string userLogin)
        {
            return await _dbContext.Set<PostEntity>().Where(p => p.UserLogin == userLogin)
                .Select(post => Mapper.Map<PostEntity, PostModel>(post)).ToListAsync();
        }

        public async Task Update(PostModel entity)
        {
            var post = _dbContext.Set<PostEntity>().Where(a => a.Id == entity.Id).FirstOrDefault();
            if (post != null)
            {
                post = Mapper.Map<PostModel, PostEntity>(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
