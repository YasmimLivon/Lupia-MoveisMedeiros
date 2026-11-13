using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Models;

namespace MoveisMedeiros.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<Usuario> Usuarios { get; set; } 
        public DbSet<Ocorrencia> Ocorrencias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Montador> Montadores { get; set; }
        public DbSet<Arquiteto> Arquitetos { get; set; }
        public DbSet<Obra> Obras { get; set; }
        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<ItensObra> ItensObras { get; set; }
        public DbSet<ResolucaoOcorrencia> ResolucaoOcorrencias { get; set; }


        //Para não dar erro no montadorId e ObraId
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ocorrencia>()
                .HasOne(o => o.Montador)
                .WithMany()
                .HasForeignKey(o => o.MontadorId)
                .OnDelete(DeleteBehavior.Restrict);
             
            modelBuilder.Entity<ResolucaoOcorrencia>()
            .HasOne(r => r.Montador)
            .WithMany()
            .HasForeignKey(r => r.MontadorId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
