using CRM.Business.DTOs;
using CRM.Business.Services.Interfaces;
using CRM.Data.Entities;
using CRM.Data.Repositories.Interfaces;

namespace CRM.Business.Services;

public class PositionService : IPositionService
{
  private readonly IRepository<Position> _positionRepository;

  public PositionService(IRepository<Position> positionRepository)
  {
    _positionRepository = positionRepository;
  }

  public async Task<IEnumerable<PositionDto>> GetAllPositionsAsync()
  {
    var positions = await _positionRepository.GetAllAsync();
    return positions.Select(p => new PositionDto
    {
      Id = p.Id,
      Title = p.Title,
      Level = p.Level,
      DepartmentId = p.DepartmentId
    });
  }

  public async Task<PositionDto> GetPositionByIdAsync(int id)
  {
    var position = await _positionRepository.GetByIdAsync(id);
    if (position == null)
      throw new Exception("Position not found");

    return new PositionDto
    {
      Id = position.Id,
      Title = position.Title,
      Level = position.Level,
      DepartmentId = position.DepartmentId
    };
  }

  public async Task<PositionDto> CreatePositionAsync(PositionDto positionDto)
  {
    var position = new Position
    {
      Title = positionDto.Title,
      Level = positionDto.Level,
      DepartmentId = positionDto.DepartmentId
    };

    var createdPosition = await _positionRepository.AddAsync(position);

    return new PositionDto
    {
      Id = createdPosition.Id,
      Title = createdPosition.Title,
      Level = createdPosition.Level,
      DepartmentId = createdPosition.DepartmentId
    };
  }

  public async Task UpdatePositionAsync(PositionDto positionDto)
  {
    var position = await _positionRepository.GetByIdAsync(positionDto.Id);
    if (position == null)
      throw new Exception("Position not found");

    position.Title = positionDto.Title;
    position.Level = positionDto.Level;
    position.DepartmentId = positionDto.DepartmentId;

    await _positionRepository.UpdateAsync(position);
  }

  public async Task DeletePositionAsync(int id)
  {
    await _positionRepository.DeleteAsync(id);
  }
}