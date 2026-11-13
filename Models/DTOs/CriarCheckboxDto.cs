using System.ComponentModel.DataAnnotations;

namespace MoveisMedeiros.Models.DTOs
{
    public class CriarCheckboxDto
    {
        public int ItensObraId { get; set; }
        public int MontadorId { get; set; }

        public bool Chapa { get; set; }
        public bool Cor { get; set; }
        public bool CantosBordas { get; set; }
        public bool Fitas { get; set; }
        public bool Emendas { get; set; }
        public bool Medidas { get; set; }

        public string? Descricao { get; set; }
        public string? Observacoes { get; set; }
        public bool Status { get; set; }

        public List<string> Fotos { get; set; } = new();
    }
}
