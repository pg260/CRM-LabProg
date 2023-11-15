using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infra.Mappings;

public class ProdutoCarrinhoMapping : IEntityTypeConfiguration<ProdutoCarrinho>
{
    public void Configure(EntityTypeBuilder<ProdutoCarrinho> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CarrinhoId)
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
    }
}