using ToursAPI.DTOs;

namespace ToursAPI.Services;

public interface ITourVisitService
{
    public Task<IEnumerable<TourVisitDto>> GetAllToursVisitedByUserAsync(string userId);
    public Task<IEnumerable<TourVisitDto>> GetAllUsersVisitedByTourAsync(Guid tourId);
    public Task<TourVisitDto?> GetByIdAsync(Guid id);
    public Task<TourVisitDto?> CreateAsync(TourVisitCreateDto dto);
    public Task<TourVisitDto?> UpdateAsync(Guid id, TourVisitUpdateDto dto);
}