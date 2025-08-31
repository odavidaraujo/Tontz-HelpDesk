using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tontz.Domain.Entities;

namespace Tontz.Infrastructure.Data {
    public class TontzDbContext : DbContext {
        public TontzDbContext(DbContextOptions<TontzDbContext> options) : base(options) { }
        public DbSet<TB_PLANO> TB_PLANO { get; set; }
        public DbSet<TB_EMPRESA> TB_EMPRESA { get; set; }
        public DbSet<TB_EMPRESA_PLANO> TB_EMPRESA_PLANO { get; set; }
        public DbSet<TB_FUNCIONARIO> TB_FUNCIONARIO { get; set; }
        public DbSet<TB_SETOR> TB_SETOR { get; set; }
        public DbSet<TB_CATEGORIA> TB_CATEGORIA { get; set; }
        public DbSet<TB_PRIORIDADE> TB_PRIORIDADE { get; set; }
        public DbSet<TB_STATUS> TB_STATUS { get; set; }
        public DbSet<TB_CHAMADO> TB_CHAMADO { get; set; }
        public DbSet<TB_HISTORICO_CHAMADO> TB_HISTORICO_CHAMADO { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // Deixar o EF mapear automaticamente
            // Mas já ajustando casos especiais:

            // Relacionamentos
            modelBuilder.Entity<TB_EMPRESA_PLANO>()
                .HasOne(ep => ep.EMPRESA)
                .WithOne(e => e.EMPRESA_PLANO)
                .HasForeignKey<TB_EMPRESA_PLANO>(ep => ep.ID_EMPRESA);

            modelBuilder.Entity<TB_EMPRESA_PLANO>()
                .HasOne(ep => ep.PLANO)
                .WithMany(p => p.EMPRESAS_PLANOS)
                .HasForeignKey(ep => ep.ID_PLANO);

            // TB_FUNCIONARIO -> CPF é a PK
            modelBuilder.Entity<TB_FUNCIONARIO>()
                .HasKey(f => f.CPF);

            // TB_HISTORICO_CHAMADO relacionamentos
            modelBuilder.Entity<TB_HISTORICO_CHAMADO>()
                .HasOne(h => h.RESPONSAVEL)
                .WithMany(f => f.HISTORICOS)
                .HasForeignKey(h => h.ID_RESPONSAVEL);

            modelBuilder.Entity<TB_HISTORICO_CHAMADO>()
                .HasOne(h => h.CHAMADO)
                .WithMany(c => c.HISTORICOS)
                .HasForeignKey(h => h.ID_CHAMADO);

            // TB_CHAMADO relacionamentos
            modelBuilder.Entity<TB_CHAMADO>()
                .HasOne(c => c.EMPRESA)
                .WithMany(e => e.CHAMADOS)
                .HasForeignKey(c => c.ID_EMPRESA);

            modelBuilder.Entity<TB_CHAMADO>()
                .HasOne(c => c.SOLICITANTE)
                .WithMany(f => f.CHAMADOS)
                .HasForeignKey(c => c.ID_SOLICITANTE);

            modelBuilder.Entity<TB_CHAMADO>()
                .HasOne(c => c.CATEGORIA)
                .WithMany(cat => cat.CHAMADOS)
                .HasForeignKey(c => c.ID_CATEGORIA);

            modelBuilder.Entity<TB_CHAMADO>()
                .HasOne(c => c.PRIORIDADE)
                .WithMany(p => p.CHAMADOS)
                .HasForeignKey(c => c.ID_PRIORIDADE);

            modelBuilder.Entity<TB_CHAMADO>()
                .HasOne(c => c.STATUS)
                .WithMany(s => s.CHAMADOS)
                .HasForeignKey(c => c.ID_STATUS);

        }
    }
}
