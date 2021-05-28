using System.ComponentModel.DataAnnotations;

namespace TbNeo.Api.V1.InputModels
{
    public class ProjetoEditarInputModel
    {
        public string Nome { get; set; }

        [MinLength(1, ErrorMessage = "A descrição pode ser nula ou precisa ter no mínimo 1 caractere")]
        public string Descricao { get; set; }

        public string UrlJira { get; set; }
    }
}
