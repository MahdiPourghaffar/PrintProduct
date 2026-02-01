using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PrintProduct.Domain.Common;
using PrintProduct.Domain.Entities;
using System.Reflection;

namespace PrintProduct.Infrastructure.Persistence;

public class PrintProductDbContext : DbContext, IApplicationDbContext
{
    private IDbContextTransaction? _currentTransaction;

    public DbSet<Product> Products => Set<Product>();
    public PrintProductDbContext(DbContextOptions<PrintProductDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        RegisterEntities(modelBuilder);
        ApplyEntityConfigurations(modelBuilder);
    }

    private static void ApplyEntityConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(PrintProductDbContext).Assembly);
    }

    private static void RegisterEntities(ModelBuilder modelBuilder)
    {
        var entityTypes = typeof(IEntity).Assembly
            .GetTypes()
            .Where(t =>
                t.IsClass &&
                !t.IsAbstract &&
                typeof(IEntity).IsAssignableFrom(t));

        foreach (var type in entityTypes)
        {
            modelBuilder.Entity(type);
        }
    }


    public async Task BeginTransactionAsync(CancellationToken ct = default)
    {
        if (_currentTransaction != null)
            return;

        _currentTransaction = await Database.BeginTransactionAsync(ct);
    }

    public async Task CommitTransactionAsync(CancellationToken ct = default)
    {
        try
        {
            await SaveChangesAsync(ct);
            await _currentTransaction!.CommitAsync(ct);
        }
        catch
        {
            await RollbackTransactionAsync(ct);
            throw;
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken ct = default)
    {
        try
        {
            if (_currentTransaction != null)
                await _currentTransaction.RollbackAsync(ct);
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
    }
}