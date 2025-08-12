using ToursAPI.DTOs;
using ToursAPI.Models;

namespace ToursAPI.Services;

public interface IIndustryService
{
    Task<IEnumerable<IndustryDto>> GetAllAsync();
    Task<IndustryDto?> GetByIdAsync(Guid id);
    Task<IndustryDto> CreateAsync(IndustryCreateDto dto);
    Task<IndustryDto?> UpdateAsync(Guid id, IndustryUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}