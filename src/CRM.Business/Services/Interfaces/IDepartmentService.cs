using CRM.Business.DTOs;

namespace CRM.Business.Services.Interfaces;

public interface IDepartmentService
{
   Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
   Task<DepartmentDto> GetDepartmentByIdAsync(int id);
   Task<DepartmentDto> CreateDepartmentAsync(DepartmentDto departmentDto);
   Task UpdateDepartmentAsync(DepartmentDto departmentDto);
   Task DeleteDepartmentAsync(int id);
}