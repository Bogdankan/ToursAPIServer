using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ToursAPI.Data;
using ToursAPI.DTOs;
using ToursAPI.Models;

namespace ToursAPI.Services;

public class FeedbackService : IFeedbackService
{
    
    private readonly AppDBContext _context;
    private readonly IMapper _mapper;
    
    public FeedbackService(AppDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<FeedbackDto>> GetAllByUserAsync(string userId)
    {
        var feedbacks = await _context.Feedbacks
            .Include(f => f.User)
            .Include(f => f.Tour)
            .Where(f => f.UserId == userId)
            .ToListAsync();
            
        return _mapper.Map<IEnumerable<FeedbackDto>>(feedbacks);
    }
    
    public async Task<IEnumerable<FeedbackDto>> GetAllByTourAsync(Guid tourId)
    {
        var feedbacks = await _context.Feedbacks
            .Include(f => f.User)
            .Include(f => f.Tour)
            .Where(f => f.TourId == tourId)
            .ToListAsync();
            
        return _mapper.Map<IEnumerable<FeedbackDto>>(feedbacks);
    }

    public async Task<FeedbackDto?> GetByIdAsync(Guid id)
    {
        var feedback = await _context.Feedbacks
            .Include(f => f.User)
            .Include(f => f.Tour)
            .SingleOrDefaultAsync(f => f.Id == id);
        return feedback == null ? null : _mapper.Map<FeedbackDto>(feedback);
    }

    public async Task<FeedbackDto> CreateAsync(FeedbackCreateDto dto)
    {
        var entity = _mapper.Map<Feedback>(dto);
        entity.CreatedAt = DateTime.UtcNow;
        _context.Feedbacks.Add(entity);
        await _context.SaveChangesAsync();
        return await _context.Feedbacks
            .AsNoTracking()
            .Where(f => f.Id == entity.Id)
            .ProjectTo<FeedbackDto>(_mapper.ConfigurationProvider)
            .SingleAsync();
    }

    public async Task<FeedbackDto?> UpdateAsync(Guid id, FeedbackUpdateDto  dto)
    {
        var feedback = await _context.Feedbacks.FindAsync(id);
        if (feedback == null) return null;

        _mapper.Map(dto, feedback); 
        await _context.SaveChangesAsync();
        return await _context.Feedbacks
            .AsNoTracking()
            .Where(f => f.Id == feedback.Id)
            .ProjectTo<FeedbackDto>(_mapper.ConfigurationProvider)
            .SingleAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await _context.Feedbacks.FindAsync(id);
        if (existing == null) return false;

        _context.Feedbacks.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}