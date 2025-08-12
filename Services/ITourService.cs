using ToursAPI.DTOs;
using ToursAPI.Models;

namespace ToursAPI.Services;

public interface ITourService
{
    Task<IEnumerable<TourDto>> GetAllAsync();
    Task<TourDto?> GetByIdAsync(Guid id);
    Task<TourDto> CreateAsync(TourCreateDto dto);
    Task<TourDto?> UpdateAsync(Guid id, TourUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}