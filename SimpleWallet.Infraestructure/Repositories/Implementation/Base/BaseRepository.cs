namespace SimpleWallet.Infraestructure.Repositories.Implementation.Base;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleWallet.Domain.Entities.Base;
using SimpleWallet.Infraestructure.Context;
using SimpleWallet.Infraestructure.Repositories.Interfaces.Base;

public class BaseRepository<TEntity>(SimpleWalletDbContext context) : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly SimpleWalletDbContext _context = context;

    public Task<TEntity> AddAsync(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        return Task.FromResult(entity);
    }

    public Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }

    public Task<IQueryable<TEntity>> GetAllAsync()
    {
        return Task.FromResult(_context.Set<TEntity>().AsQueryable());
    }

    public Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where)
    {
        return Task.FromResult(_context.Set<TEntity>().Where(where).AsQueryable());
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        return entity;
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        return Task.FromResult(entity);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}