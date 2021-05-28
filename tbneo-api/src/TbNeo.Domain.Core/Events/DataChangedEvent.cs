using System;
using TbNeo.Domain.Core;

namespace TbNeo.Domain.Core.Events
{
    public class DataChangedEvent : Event
    {
        public Guid Reference { get; private set; }

        public string NomeCampo { get; private set; }

        public string ValorNovo { get; private set; }

        public string ValorAntigo { get; private set; }

        public int IdAlteradoPor { get; private set; }

        public DataChangedEvent(Guid reference,
                                string nomeCampo,
                                string valorNovo,
                                string valorAntigo,
                                int idAlteradoPor)
        {
            Reference = reference;
            NomeCampo = nomeCampo;
            ValorNovo = valorNovo;
            ValorAntigo = valorAntigo;
            IdAlteradoPor = idAlteradoPor;
        }
    }
}
