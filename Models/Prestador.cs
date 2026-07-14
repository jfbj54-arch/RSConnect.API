namespace RSConnect.API.Models
{
    public class Prestador
    {
        public int id { get; set; }
        public string nome { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string senha { get; set; } = string.Empty;
        public string telefone { get; set; } = string.Empty;
        public string categoria { get; set; } = string.Empty; // eletricista, encanador, etc.
        public string endereco { get; set; } = string.Empty;
        public double nota { get; set; } = 0; // avaliação futura
    }
}
