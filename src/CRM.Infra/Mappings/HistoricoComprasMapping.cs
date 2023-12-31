﻿using CRM.Domain.Entities;
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
        
        builder.Property(c => c.ValorTotal)
            .IsRequired();

        builder.Property(c => c.MetodoDePagameto)
            .IsRequired();
        
        builder.Property(c => c.CriadoEm)
            .IsRequired()
            .HasColumnType("datetime");
        
        builder.Property(c => c.AtualizadoEm)
            .IsRequired()
            .HasColumnType("datetime");

        builder.HasMany(c => c.Compras)
            .WithOne(r => r.HistoricoCompras)
            .HasForeignKey(r => r.HistoricoCompras)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}