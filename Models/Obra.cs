namespace MoveisMedeiros.Models
{
    public class Obra
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public int ClienteId { get; set; }
        public int MontadorId { get; set; }
        public string NomeArquiteto { get; set; }
        public DateOnly DataInspecao { get; set; }
        public DateOnly Prazo { get; set; }
        public string PDF { get; set; }
        public string Status { get; set; }
    }
}
