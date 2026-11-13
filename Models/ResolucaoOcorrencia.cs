namespace MoveisMedeiros.Models
{
    public class ResolucaoOcorrencia
    {
        public int Id { get; set; }
        public int OcorrenciaId { get; set; }
        public int MontadorId { get; set; }
        public string AcaoTomada { get; set; } = string.Empty;
        public DateOnly PrazoResolucao { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime DataResolucao { get; set; } = DateTime.Now;
        public string FotoResolucao { get; set; } = string.Empty;

        public Ocorrencia? Ocorrencia { get; set; }
        public Montador? Montador { get; set; }
    }
}
