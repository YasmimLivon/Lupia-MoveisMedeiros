namespace MoveisMedeiros.Models.DTOs
{
    public class CriarOcorrenciaDto
    {
        public int ObraId { get; set; }
        public int MontadorId { get; set; }
        public string FotoOcorrencia { get; set; }
        public string Local { get; set; }
        public string Descricao { get; set; }
    }
}
