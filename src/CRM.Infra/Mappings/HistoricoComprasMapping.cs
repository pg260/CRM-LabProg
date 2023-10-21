using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infra.Mappings;

public class HistoricoComprasMapping : IEntityTypeConfiguration<HistoricoCompras>
{
    public void Configure(EntityTypeBuilder<HistoricoCompras> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.UserId)
            .IsRequired();
        
        builder.Property(c => c.ValorHistorico)
            .IsRequired();
        
        builder.Property(c => c.CriadoEm)
            .IsRequired();
        
        builder.Property(c => c.AtualizadoEm)
            .IsRequired();

        builder.HasMany(c => c.Compras)
            .WithOne(r => r.HistoricoCompras)
            .HasForeignKey(r => r.HistoricoId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}