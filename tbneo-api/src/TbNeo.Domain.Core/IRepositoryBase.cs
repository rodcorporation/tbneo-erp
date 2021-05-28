using System.Threading.Tasks;

namespace TbNeo.Domain.Core
{
    public interface IRepositoryBase<T> where T : Entity
    {
        IUnitOfWork UnitOfWork { get; }

        Task Add(T entity);

        Task<T> Get(int id);

        void Update(T entity);

        void Remove(T entity);   
    }
}
