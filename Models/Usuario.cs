namespace RSConnect.API.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string nome { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
