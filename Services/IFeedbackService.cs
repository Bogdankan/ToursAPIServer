using ToursAPI.DTOs;

namespace ToursAPI.Services;

public interface IFeedbackService
{
    public Task<IEnumerable<FeedbackDto>> GetAllByUserAsync(string userId);
    public Task<IEnumerable<FeedbackDto>> GetAllByTourAsync(Guid tourId);
    public Task<FeedbackDto?> GetByIdAsync(Guid id);
    public Task<FeedbackDto> CreateAsync(FeedbackCreateDto dto);
    public Task<FeedbackDto?> UpdateAsync(Guid id, FeedbackUpdateDto dto);
    public Task<bool> DeleteAsync(Guid id);
}