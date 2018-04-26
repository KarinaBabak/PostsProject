using System;
using System.Threading.Tasks;
using Posts.Model.Interface;
using Posts.Persistence.Interface;

namespace Posts.Application
{
    public abstract class BaseTextService<TModel> where TModel : IText
    {
        protected readonly IRepository<TModel> Repository;

        protected BaseTextService(IRepository<TModel> repository)
        {
            Repository = repository;
        }

        public async Task<int> Create(TModel model)
        {
            CheckText(model);
            return await Repository.Add(model);
        }

        public async Task Delete(TModel model)
        {
            CheckModelExisting(model);
            var existedModel = await GetById(model.Id);

            if (existedModel != null)
            {
                throw new ArgumentException($"The entity { nameof(model) } is not found");
            }

            await Repository.Delete(model);
        }

        public async Task Update(TModel model)
        {
            CheckText(model);

            await Repository.Update(model);
        }

        public async Task<TModel> GetById(int modelId)
        {
            return await Repository.GetById(modelId);
        }

        private void CheckText(TModel model)
        {
            CheckModelExisting(model);
            CheckTextAuthor(model);
            CheckTextContent(model);
        }

        private void CheckModelExisting(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException($"The entity { nameof(model) } can not be null");
            }
        }

        private void CheckTextContent(TModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Content))
            {
                throw new ArgumentException($"The content { nameof(model) } does not exist");
            }
        }

        private void CheckTextAuthor(TModel model)
        {
            if (string.IsNullOrWhiteSpace(model.UserLogin))
            {
                throw new ArgumentException($"The text author { nameof(model) } can not be anonymous");
            }
        }
    }
}
