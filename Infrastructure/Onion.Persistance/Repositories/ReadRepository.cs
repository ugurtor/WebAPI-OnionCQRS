﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Onion.Application.Interfaces.Repositories;
using Onion.Domain.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Onion.Persistance.Repositories;
public class ReadRepository<T> : IReadRepository<T> where T : class, IEntityBase, new()
{
    private readonly DbContext dbContext;
    public ReadRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    private DbSet<T> Table { get => dbContext.Set<T>(); }

    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
    {
        Table.AsNoTracking();
        if (predicate is not null)
            Table.Where(predicate);

        return await Table.CountAsync();
    }

    public IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false)
    {
        if (!enableTracking)
            Table.AsNoTracking();

        return Table.Where(predicate);
    }

    public async Task<IList<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool enableTracking = true)
    {
        IQueryable<T> queryable = Table;
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include is not null)
            queryable = include(queryable);
        if (predicate is not null)
            queryable = queryable.Where(predicate);
        if (orderBy is not null)
            return await orderBy(queryable).ToListAsync();

        return await queryable.ToListAsync();
    }

    public async Task<IList<T>> GetAllByPagingAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool enableTracking = false,
        int currentPage = 1,
        int pageSize = 5)
    {
        IQueryable<T> queryable = Table;
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include is not null)
            queryable = include(queryable);
        if (predicate is not null)
            queryable = queryable.Where(predicate);
        if (orderBy is not null)
            return await orderBy(queryable).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

        return await queryable.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<T> GetAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool enableTracking = false)
    {
        IQueryable<T> queryable = Table;
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include is not null)
            queryable = include(queryable);

        return await queryable.SingleOrDefaultAsync(predicate);
    }
}
