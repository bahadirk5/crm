using CRM.Business.DTOs;

namespace CRM.Business.Services.Interfaces;

public interface IPositionService
{
   Task<IEnumerable<PositionDto>> GetAllPositionsAsync();
   Task<PositionDto> GetPositionByIdAsync(int id);
   Task<PositionDto> CreatePositionAsync(PositionDto positionDto);
   Task UpdatePositionAsync(PositionDto positionDto);
   Task DeletePositionAsync(int id);
}