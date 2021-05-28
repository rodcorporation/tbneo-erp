using System;
using System.Collections.Generic;
using TbNeo.Domain.Core;
using TbNeo.Domain.Core.Events;

namespace TbNeo.Domain.Entities
{
    public class Projeto : Entity
    {
        #region Propriedades
        
        public string Nome { get; private set; }

        public string Descricao { get; private set; }

        // Na dúvida, vou deixar como obrigatório
        public string UrlJira { get; private set; }

        public Usuario CriadoPor { get; private set; }

        public DateTime CriadoEm { get; set; }

        public Usuario AtualizadoPor { get; private set; }

        public DateTime? AtualizadoEm { get; set; }

        private List<FeatureFlag> _featureFlags;
        public IReadOnlyList<FeatureFlag> FeatureFlags => _featureFlags;

        // For EF Core

        public int IdCriadoPor { get; set; }

        public int? IdAtualizadoPor { get; set; } 

        #endregion

        #region Construtores

        protected Projeto()
        {
            _featureFlags = new List<FeatureFlag>();
        }

        public Projeto(string nome,
                       string descricao,
                       string urlJira,
                       Usuario criadoPor)
        {
            Nome = nome;
            Descricao = descricao;
            UrlJira = urlJira;

            CriadoPor = criadoPor;
            IdCriadoPor = criadoPor.Id;

            CriadoEm = DateTime.Now;

            _featureFlags = new List<FeatureFlag>();
        } 

        #endregion

        #region Métodos

        public void Alterar(string nome,
                            string descricao,
                            string urlJira,
                            Usuario atualizadoPor)
        {
            if (!Nome.Equals(nome))
            {
                _events.Add(new DataChangedEvent(this.IdLogReference,
                                                 nameof(Nome),
                                                 nome,
                                                 Nome,
                                                 atualizadoPor.Id));
            }

            Nome = nome;

            if (!Descricao.Equals(descricao))
            {
                _events.Add(new DataChangedEvent(this.IdLogReference,
                                                 nameof(Descricao),
                                                 descricao,
                                                 Descricao,
                                                 atualizadoPor.Id));
            }

            Descricao = descricao;

            if (!UrlJira.Equals(urlJira))
            {
                _events.Add(new DataChangedEvent(this.IdLogReference,
                                                 nameof(UrlJira),
                                                 urlJira,
                                                 UrlJira,
                                                 atualizadoPor.Id));
            }

            UrlJira = urlJira;

            AtualizadoPor = atualizadoPor;
            IdAtualizadoPor = atualizadoPor.Id;

            AtualizadoEm = DateTime.Now;
        } 

        #endregion
    }
}
