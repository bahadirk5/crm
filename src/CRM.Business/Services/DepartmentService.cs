using CRM.Business.DTOs;
using CRM.Business.Services.Interfaces;
using CRM.Data.Entities;
using CRM.Data.Repositories.Interfaces;

namespace CRM.Business.Services;

public class DepartmentService : IDepartmentService
{
  private readonly IRepository<Department> _departmentRepository;

  public DepartmentService(IRepository<Department> departmentRepository)
  {
    _departmentRepository = departmentRepository;
  }

  public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
  {
    var departments = await _departmentRepository.GetAllAsync();
    return departments.Select(d => new DepartmentDto
    {
      Id = d.Id,
      Name = d.Name,
    });
  }
  public async Task<DepartmentDto> GetDepartmentByIdAsync(int id)
  {
    var department = await _departmentRepository.GetByIdAsync(id);
    if (department == null)
      throw new Exception("Department not found");

    return new DepartmentDto
    {
      Id = department.Id,
      Name = department.Name,
    };
  }

  public async Task<DepartmentDto> CreateDepartmentAsync(DepartmentDto departmentDto)
  {
    var department = new Department
    {
      Name = departmentDto.Name,
    };

    var createdDepartment = await _departmentRepository.AddAsync(department);

    return new DepartmentDto
    {
      Id = createdDepartment.Id,
      Name = createdDepartment.Name,
    };
  }

  public async Task UpdateDepartmentAsync(DepartmentDto departmentDto)
  {
    var department = await _departmentRepository.GetByIdAsync(departmentDto.Id);
    if (department == null)
      throw new Exception("Department not found");

    department.Name = departmentDto.Name;

    await _departmentRepository.UpdateAsync(department);
  }

  public async Task DeleteDepartmentAsync(int id)
  {
    await _departmentRepository.DeleteAsync(id);
  }
}