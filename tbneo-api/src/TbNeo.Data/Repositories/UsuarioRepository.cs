using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TbNeo.Domain.Entities;
using TbNeo.Domain.Repositories;

namespace TbNeo.Data.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(TbNeoContext context) : base(context)
        {
        }

        public async Task<Usuario> GetByEmail(string email)
        {
            return await 
                        _context
                            .Usuarios
                            .SingleOrDefaultAsync(p => p.Email == email);
        }
    }
}
