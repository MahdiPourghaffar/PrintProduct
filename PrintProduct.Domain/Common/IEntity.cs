using System;
using System.Collections.Generic;
using System.Text;

namespace PrintProduct.Domain.Common;

public interface IEntity
{
}

public interface IEntity<TKey> : IEntity
{
    TKey Id { get; }
}
public abstract class Entity<TKey> : IEntity<TKey>
{
    public TKey Id { get; protected set; } = default!;
}
