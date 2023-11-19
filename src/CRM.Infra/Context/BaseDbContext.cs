using System.ComponentModel.DataAnnotations;
using CRM.Core.Authorization;
using CRM.Domain.Contracts;
using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRM.Infra.Context;

public class BaseDbContext : DbContext, IUnitOfWork
{
    public BaseDbContext(DbContextOptions<BaseDbContext> options, IAuthenticatedUser authenticatedUser) : base(options)
    { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Carrinho> Carrinhos { get; set; } = null!;
    public DbSet<Compra> Compras { get; set; }  = null!;
    public DbSet<Feedback> Feedbacks { get; set; }  = null!;
    public DbSet<HistoricoCompras> HistoricoCompras { get; set; }  = null!;
    public DbSet<Produto> Produtos { get; set; } = null!;
    public DbSet<ProdutoCarrinho> ProdutoCarrinhos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureDatabaseSettings(modelBuilder);
        ConfigureEntities(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private void ConfigureDatabaseSettings(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasCharSet("utf8mb4")
            .UseCollation("utf8mb4_0900_ai_ci")
            .UseGuidCollation(string.Empty);
    }

    private void ConfigureEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        ConfigureDateAndTimeProperties(modelBuilder);
    }

    private void ConfigureDateAndTimeProperties(ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.Model.GetEntityTypes()
            .Where(e => typeof(BaseEntity).IsAssignableFrom(e.ClrType));

        foreach (var entityType in entityTypes)
        {
            var properties = entityType.GetProperties();

            foreach (var property in properties)
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetColumnType("datetime");
                    property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                        v => v,
                        v => v
                    ));
                }
            }
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyTrackingChanges();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyTrackingChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((BaseEntity)entityEntry.Entity).AtualizadoEm = DateTime.Now;

            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).CriadoEm = DateTime.Now;
            }
        }
    }

    public async Task<bool> Commit() => await SaveChangesAsync() > 0;
}