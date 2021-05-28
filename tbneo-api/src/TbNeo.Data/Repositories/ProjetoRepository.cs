using TbNeo.Domain.Entities;
using TbNeo.Domain.Repositories;

namespace TbNeo.Data.Repositories
{
    public class ProjetoRepository : RepositoryBase<Projeto>, IProjetoRepository
    {
        public ProjetoRepository(TbNeoContext context) : base(context)
        {
        }
    }
}
