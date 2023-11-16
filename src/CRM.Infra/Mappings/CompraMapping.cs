using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infra.Mappings;

public class CompraMapping : IEntityTypeConfiguration<Compra>
{
    public void Configure(EntityTypeBuilder<Compra> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.HistoricoId)
            .IsRequired();
        
        builder.Property(c => c.UserId)
            .IsRequired();
        
        builder.Property(c => c.ProdutoId)
            .IsRequired();
        
        builder.Property(c => c.Quantidade)
            .IsRequired();
        
        builder.Property(c => c.ValorTotal)
            .IsRequired();
        
        builder.Property(c => c.CriadoEm)
            .IsRequired()
            .HasColumnType("datetime");
        
        builder.Property(c => c.AtualizadoEm)
            .IsRequired()
            .HasColumnType("datetime");
        
        builder.HasMany(p => p.Feedbacks)
            .WithOne(c => c.Compra)
            .HasForeignKey(c => c.CompraId)
            .HasPrincipalKey(p => p.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}