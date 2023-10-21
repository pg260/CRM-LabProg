using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infra.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CarrinhoId);

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(25);
        
        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(80);

        builder.Property(c => c.Senha)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(c => c.Foto);

        builder.Property(c => c.CriadoEm)
            .IsRequired();

        builder.Property(c => c.AtualizadoEm)
            .IsRequired();

        builder.HasMany(c => c.HistoricoCompras)
            .WithOne(h => h.User)
            .HasForeignKey(h => h.UserId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Compras)
            .WithOne(h => h.User)
            .HasForeignKey(h => h.UserId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(c => c.Feedbacks)
            .WithOne(h => h.User)
            .HasForeignKey(h => h.UserId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(c => c.Produtos)
            .WithOne(h => h.User)
            .HasForeignKey(h => h.UserId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}