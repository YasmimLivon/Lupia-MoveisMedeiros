namespace MoveisMedeiros.Models.DTOs
{
    public class CriarItemObra
    {
        public int ObraId { get; set; }
        public string NomeItem { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public string Cor { get; set; }
        public string Local { get; set; }
        public string Status { get; set; }
        public string Descricao { get; set; }
        public string Foto { get; set; }
    }
}
