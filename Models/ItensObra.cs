namespace MoveisMedeiros.Models
{
    public class ItensObra
    {
        public int Id { get; set; }
        public int ObraId { get; set; }
        public string NomeItem { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public string Cor { get; set; }
        public string Local { get; set; }
        public string Status { get; set; }
        public string? Descricao { get; set; }
        public string? Foto { get; set; }

        public Obra? Obra { get; set; }
    }
}
