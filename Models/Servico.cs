namespace RSConnect.API.Models
{
    public class Servico
    {
        public int id { get; set; }
        public int clienteId { get; set; }
        public int? prestadorId { get; set; }
        public string categoria { get; set; } = string.Empty;
        public string descricao { get; set; } = string.Empty;
        public string endereco { get; set; } = string.Empty;
        public DateTime dataHora { get; set; }
        public string status { get; set; } = "Solicitado";
    }
}
