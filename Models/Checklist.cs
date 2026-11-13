using System.Reflection.Metadata.Ecma335;

namespace MoveisMedeiros.Models
{
    public class Checklist
    {
        public int Id { get; set; }
        public int ItensObraId { get; set; }

        public bool Cor { get; set; }
        public bool Medidas { get; set; }
        public bool Status { get; set; }
        public bool Chapa { get; set; }
        public bool CantosBordas { get; set; }
        public bool Fitas { get; set; }
        public bool Emendas { get; set; }

        public string? Descricao { get; set; }
        public string? Observacoes { get; set; }

        public List<string> Fotos { get; set; } = new();
        public ItensObra? ItensObra { get; set; }

    }
}
