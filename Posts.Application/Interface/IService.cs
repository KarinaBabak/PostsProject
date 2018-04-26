using System.Threading.Tasks;

namespace Posts.Application.Interface
{
    public interface IService<TModel>
    {
        Task<int> Create(TModel model);
        Task Update(TModel model);
        Task Delete(TModel model);
        Task<TModel> GetById(int modelId);
    }
}
