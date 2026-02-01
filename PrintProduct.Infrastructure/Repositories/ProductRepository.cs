using PrintProduct.Application.Contracts.Persistence;
using PrintProduct.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PrintProduct.Infrastructure.Persistence;

namespace PrintProduct.Infrastructure.Repositories;

public class ProductRepository(IApplicationDbContext context) : IProductRepository
{
    private readonly IApplicationDbContext _context = context;

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Product product, CancellationToken cancellationToken = default)
    {
        product.MarkAsDeleted();
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Where(p => !p.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    // --------------------
    // Transaction-aware operations
    // --------------------

    /// <summary>
    /// Executes multiple operations in a transaction.
    /// Rollback occurs automatically if any exception is thrown.
    /// </summary>
    public async Task ExecuteInTransactionAsync(Func<Task> operations, CancellationToken cancellationToken = default)
    {
        await _context.BeginTransactionAsync(cancellationToken);

        try
        {
            await operations.Invoke();
            await _context.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await _context.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    /// <summary>
    /// Executes multiple operations in a transaction with result.
    /// </summary>
    public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operations, CancellationToken cancellationToken = default)
    {
        await _context.BeginTransactionAsync(cancellationToken);

        try
        {
            var result = await operations.Invoke();
            await _context.CommitTransactionAsync(cancellationToken);
            return result;
        }
        catch
        {
            await _context.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}