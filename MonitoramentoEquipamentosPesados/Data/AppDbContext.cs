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

        // DbSet pode continuar plural ou singular, não importa
        public DbSet<Equipamento> Equipamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeia explicitamente a tabela para bater com o nome no PostgreSQL
            modelBuilder.Entity<Equipamento>()
                .ToTable("Equipamento"); // ← nome exato da tabela no banco
        }
    }
}