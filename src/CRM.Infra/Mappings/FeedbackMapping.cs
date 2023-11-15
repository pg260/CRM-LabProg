using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infra.Mappings;

public class FeedbackMapping : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.UserId)
            .IsRequired();
        
        builder.Property(c => c.ProdutoId)
            .IsRequired();
        
        builder.Property(c => c.CompraId)
            .IsRequired();
        
        builder.Property(c => c.Avaliacao)
            .IsRequired();

        builder.Property(c => c.Comentarios)
            .HasMaxLength(200);
        
        builder.Property(c => c.CriadoEm)
            .IsRequired()
            .HasColumnType("datetime");
        
        builder.Property(c => c.AtualizadoEm)
            .IsRequired()
            .HasColumnType("datetime");
    }
}