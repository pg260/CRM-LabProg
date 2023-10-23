using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infra.Mappings;

public class CarrinhoMapping : IEntityTypeConfiguration<Carrinho>
{
    public void Configure(EntityTypeBuilder<Carrinho> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.UserId)
            .IsRequired();

        builder.Property(c => c.ValorTotal)
            .IsRequired();

        builder.Property(c => c.CriadoEm)
            .IsRequired()
            .HasColumnType("datetime");;
        
        builder.Property(c => c.AtualizadoEm)
            .IsRequired()
            .HasColumnType("datetime");;
        
        builder.HasMany(c => c.ProdutoCarrinhos)
            .WithOne(r => r.Carrinho)
            .HasForeignKey(r => r.CarrinhoId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}