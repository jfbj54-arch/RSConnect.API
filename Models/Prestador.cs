namespace RSConnect.API.Models
{
    public class Prestador
    {
        public int id { get; set; }
        public string nome { get; set; } = string.Empty;
        public string telefone { get; set; } = string.Empty;
        public string categoria { get; set; } = string.Empty;

        // CAMPOS NECESSÁRIOS PARA LOGIN
        public string email { get; set; } = string.Empty;
        public string senha { get; set; } = string.Empty;
    }
}
