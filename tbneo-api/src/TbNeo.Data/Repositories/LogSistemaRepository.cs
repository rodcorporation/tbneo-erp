using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TbNeo.Domain.Core;
using TbNeo.Domain.Entities;
using TbNeo.Domain.Repositories;

namespace TbNeo.Data.Repositories
{
    public class LogSistemaRepository : ILogSistemaRepository
    {
        private TbNeoContext _context;

        public LogSistemaRepository(TbNeoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Add(LogSistema logSistema)
        {
            await _context.LogSistemas.AddAsync(logSistema);
        }

        public async Task<IEnumerable<LogSistema>> ListByReference(Guid reference)
        {
            return await
                        _context
                            .LogSistemas
                            .Where(p => p.Reference == reference)
                            .ToListAsync();
        }
    }
}
