using HidroSafe.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HidroSafe.API.Data
{
    /// <summary>
    /// Contexto de banco de dados principal da aplicação HidroSafe.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Representa a tabela de estações de monitoramento.
        /// </summary>
        public DbSet<EstacaoMonitoramento> Estacoes { get; set; }

        /// <summary>
        /// Representa a tabela de leituras de distância da água.
        /// </summary>
        public DbSet<LeituraDistancia> Leituras { get; set; }

        /// <summary>
        /// Configura o modelo das entidades e popula dados iniciais.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeia nomes de tabelas
            modelBuilder.Entity<EstacaoMonitoramento>().ToTable("ESTACOES");
            modelBuilder.Entity<LeituraDistancia>().ToTable("LEITURAS");

            // Configura relacionamento 1:N entre Estação e Leitura
            modelBuilder.Entity<LeituraDistancia>()
                .HasOne(l => l.EstacaoMonitoramento)
                .WithMany(e => e.Leituras)
                .HasForeignKey(l => l.EstacaoMonitoramentoId);

            // Popula dados iniciais
            Seed(modelBuilder);
        }

        /// <summary>
        /// Adiciona dados iniciais ao banco (seeding).
        /// </summary>
        private static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EstacaoMonitoramento>().HasData(
                new EstacaoMonitoramento
                {
                    Id = 1,
                    Nome = "Estação Rio do Meio",
                    Localizacao = "Rua do Córrego, 123",
                    DataInstalacao = new DateTime(2025, 6, 1)
                }
            );

            modelBuilder.Entity<LeituraDistancia>().HasData(
                new LeituraDistancia
                {
                    Id = 1,
                    DataHora = new DateTime(2025, 6, 8, 14, 30, 0),
                    DistanciaMargemCm = 18.5,
                    EstacaoMonitoramentoId = 1
                    
                }
            );
        }
    }
}
