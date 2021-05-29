﻿using System;

namespace TbNeo.Application.Queries.QueryModels
{
    public class FeatureFlagListagemQueryModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string IdProjeto { get; set; }

        public string NomeProjeto { get; set; }

        public Guid IdLogReference { get; set; }
    }
}
