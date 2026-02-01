using Microsoft.EntityFrameworkCore;
using PrintProduct.Domain.Entities;
using System.Collections.Generic;

namespace PrintProduct.Infrastructure.Persistence;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}