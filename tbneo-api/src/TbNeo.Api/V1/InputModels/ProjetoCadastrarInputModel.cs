namespace TbNeo.Api.V1.InputModels
{
    public class ProjetoCadastrarInputModel
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string UrlJira { get; set; }

        public int IdUsuario { get; set; }
    }
}
