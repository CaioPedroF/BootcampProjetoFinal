using Microsoft.EntityFrameworkCore;
using MonitoramentoEquipamentosPesados.Models;

namespace MonitoramentoEquipamentosPesados.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet plural para código C#, mapeando para tabela singular "Equipamento"
        public DbSet<Equipamento> Equipamentos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeia explicitamente para a tabela existente no PostgreSQL
            modelBuilder.Entity<Equipamento>().ToTable("Equipamento");
        }
    }
}
