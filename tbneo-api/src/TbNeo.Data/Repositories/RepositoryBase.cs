using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TbNeo.Domain.Core;

namespace TbNeo.Data.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : Entity
    {
        protected TbNeoContext _context;

        public RepositoryBase(TbNeoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task<T> Get(int id)
        {
            return await _context
                            .Set<T>()
                            .SingleOrDefaultAsync(p => p.Id == id);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
