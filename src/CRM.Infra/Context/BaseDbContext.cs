using System.ComponentModel.DataAnnotations;
using CRM.Domain.Contracts;
using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infra.Context;

public class BaseDbContext : DbContext, IUnitOfWork
{
    public BaseDbContext(DbContextOptions<DbContext> options) : base(options)
    {}

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Carrinho> Carrinhos { get; set; } = null!;
    public DbSet<Compra> Compras { get; set; }  = null!;
    public DbSet<Feedback> Feedbacks { get; set; }  = null!;
    public DbSet<HistoricoCompras> HistoricoCompras { get; set; }  = null!;
    public DbSet<Produto> Produtos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasCharSet("utf8mb4")
            .UseCollation("utf8mb4_0900_ai_ci")
            .UseGuidCollation(string.Empty);
        
        modelBuilder.Ignore<ValidationResult>();
        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit() => await SaveChangesAsync() > 0;
}