using CRM.Data.Common;

namespace CRM.Data.Repositories.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
  Task<List<T>> GetAllAsync();
  Task<T> GetByIdAsync(int id);
  Task<T> AddAsync(T entity);
  Task UpdateAsync(T entity);
  Task DeleteAsync(int id);
}