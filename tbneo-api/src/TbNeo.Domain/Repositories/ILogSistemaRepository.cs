using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TbNeo.Domain.Core;
using TbNeo.Domain.Entities;

namespace TbNeo.Domain.Repositories
{
    public interface ILogSistemaRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Task Add(LogSistema logSistema);

        Task<IEnumerable<LogSistema>> ListByReference(Guid reference);
    }
}
