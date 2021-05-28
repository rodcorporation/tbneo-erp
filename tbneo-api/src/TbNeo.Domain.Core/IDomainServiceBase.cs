using System.Threading.Tasks;

namespace TbNeo.Domain.Core
{
    public interface IDomainServiceBase<T> where T : Entity
    {
        Task Add(T entity);

        Task<T> Get(int id);

        void Update(T entity);

        void Remove(T entity);   
    }
}
