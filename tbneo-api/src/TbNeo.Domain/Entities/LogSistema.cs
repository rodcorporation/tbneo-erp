using System;
using TbNeo.Domain.Core;

namespace TbNeo.Domain.Entities
{
    public class LogSistema : Entity
    {
        public Guid Reference { get; set; }

        public string NomeCampo { get; private set; }

        public string ValorNovo { get; private set; }

        public string ValorAntigo { get; private set; }

        public Usuario AlteradoPor { get; private set; }

        public DateTime AlteradoEm { get; private set; }

        // For EF Core

        public int IdAlteradoPor { get; private set; }

        protected LogSistema() { }

        public LogSistema(Guid reference,
                          string nomeCampo,
                          string valorNovo,
                          string valorAntigo,
                          Usuario alteradoPor)
        {
            Reference = reference;

            NomeCampo = nomeCampo;

            ValorNovo = valorNovo;
            ValorAntigo = valorAntigo;

            AlteradoPor = alteradoPor;
            IdAlteradoPor = alteradoPor.Id;

            AlteradoEm = DateTime.Now;
        }

        public override string ToString()
        {
            return $"O valor do campo {NomeCampo} foi preenchido como {ValorNovo} por {AlteradoPor.Nome.ToUpper()}";
        }
    }
}
