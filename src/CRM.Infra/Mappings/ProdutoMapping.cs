using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infra.Mappings;

public class ProdutoMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.UserId)
            .IsRequired();

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(30);
        
        builder.Property(c => c.Descricao)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(c => c.Valor)
            .IsRequired();

        builder.Property(c => c.Nota);

        builder.Property(c => c.Cidade)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Estado)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(c => c.CriadoEm)
            .IsRequired()
            .HasColumnType("datetime");
        
        builder.Property(c => c.AtualizadoEm)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(c => c.Desativado)
            .IsRequired();

        builder.HasMany(p => p.ProdutoCarrinhos)
            .WithOne(c => c.Produto)
            .HasForeignKey(c => c.ProdutoId)
            .HasPrincipalKey(p => p.Id)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(p => p.Feedbacks)
            .WithOne(c => c.Produto)
            .HasForeignKey(c => c.ProdutoId)
            .HasPrincipalKey(p => p.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}