using System.Reflection.Metadata.Ecma335;

namespace MoveisMedeiros.Models
{
    public class Ocorrencia
    {
        public int Id { get; set; }
        public int ObraId { get; set; }
        public int MontadorId { get; set; }
        public string FotoOcorrencia { get; set; }
        public string Local { get; set; }
        public string Descricao { get; set; }
    }
}
