namespace MoveisMedeiros.Models
{
    public class Checklist
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Boolean Cor { get; set; }
        public Boolean Medidas { get; set; }
        public string Descricao { get; set; }
        public Boolean Status { get; set; }
        public Boolean Chapa { get; set; }
        public Boolean CantosBordas { get; set; }
        public Boolean Fitas { get; set; }
        public Boolean Emendas { get; set; }

    }
}
