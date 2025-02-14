using CRM.Data.Context;
using CRM.Data.Entities;
using CRM.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.Data.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
  private readonly AppDbContext _context;

  public EmployeeRepository(AppDbContext context) : base(context)
  {
    _context = context;
  }

  public async Task<Employee?> GetByEmailAsync(string email)
  {
    return await _context.Employees
        .FirstOrDefaultAsync(e => e.Email == email);
  }

  public async Task<Employee?> GetByEmailWithPositionAsync(string email)
  {
    return await _context.Employees
        .Include(e => e.Position)
        .FirstOrDefaultAsync(e => e.Email == email);
  }

  public async Task<bool> EmailExistsAsync(string email)
  {
    return await _context.Employees
        .AnyAsync(e => e.Email == email);
  }
}