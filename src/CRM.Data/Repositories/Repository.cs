using CRM.Data.Context;
using CRM.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using CRM.Data.Common;

namespace CRM.Data.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
  private readonly AppDbContext _context;
  private readonly DbSet<T> _dbSet;

  public Repository(AppDbContext context)
  {
    _context = context;
    _dbSet = context.Set<T>();
  }

  public async Task<List<T>> GetAllAsync()
  {
    return await _dbSet.ToListAsync();
  }

  public async Task<T> GetByIdAsync(int id)
  {
    return await _dbSet.FindAsync(id)
        ?? throw new KeyNotFoundException($"Entity with id {id} not found");
  }
  public async Task<T> AddAsync(T entity)
  {
    await _dbSet.AddAsync(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task UpdateAsync(T entity)
  {
    _context.Entry(entity).State = EntityState.Modified;
    await _context.SaveChangesAsync();
  }

  public async Task DeleteAsync(int id)
  {
    var entity = await GetByIdAsync(id);
    _dbSet.Remove(entity);
    await _context.SaveChangesAsync();
  }
}