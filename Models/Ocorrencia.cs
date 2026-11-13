using System.Reflection.Metadata.Ecma335;

namespace MoveisMedeiros.Models
{
    public class Ocorrencia
    {
        public int Id { get; set; }
        public int ObraId { get; set; }
        public int MontadorId { get; set; }
        public string FotoOcorrencia { get; set; } = string.Empty;
        public string Local { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateOnly Prazo { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public bool Resolvida { get; set; } = false;

        public Obra? Obra { get; set; }
        public Montador? Montador { get; set; }
    }
}
