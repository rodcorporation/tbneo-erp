using System;

namespace TbNeo.Application.Queries.QueryModels
{
    public class ProjetoListagemQueryModel
    {
        public int Id { get; private set; }

        public string Nome { get; private set; }
        
        public string Descricao { get; private set; }
        
        public string UrlJira { get; private set; }

        public string CriadoPor { get; private set; }

        public DateTime CriadoEm { get; private set; }

        public string AtualizadoPor { get; private set; }

        public DateTime? AtualizadoEm { get; private set; }
    }
}
