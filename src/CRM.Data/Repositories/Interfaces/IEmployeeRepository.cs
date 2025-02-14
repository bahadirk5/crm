using CRM.Data.Entities;

namespace CRM.Data.Repositories.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
  Task<Employee?> GetByEmailAsync(string email);
  Task<Employee?> GetByEmailWithPositionAsync(string email);
  Task<bool> EmailExistsAsync(string email);
}