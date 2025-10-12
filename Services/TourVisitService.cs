using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ToursAPI.Data;
using ToursAPI.DTOs;
using ToursAPI.Models;

namespace ToursAPI.Services;

public class TourVisitService : ITourVisitService
{
    private readonly AppDBContext _context;
    private readonly IMapper _mapper;

    public TourVisitService(AppDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<TourVisitDto>> GetAllToursVisitedByUserAsync(string userId)
    {
        var tours = await _context.TourVisits
            .Include(tv => tv.Tour)
            .Include(tv => tv.User)
            .Where(tv => tv.UserId == userId)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<TourVisitDto>>(tours);
    }

    public async Task<IEnumerable<TourVisitDto>> GetAllUsersVisitedByTourAsync(Guid tourId)
    {
        var users = await _context.TourVisits
            .Include(tv => tv.Tour)
            .Include(tv => tv.User)
            .Where(tv => tv.TourId == tourId)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<TourVisitDto>>(users);
    }

    public async Task<TourVisitDto?> GetByIdAsync(Guid id)
    {
        var tourVisit = await _context.TourVisits
            .Include(tv => tv.User)
            .Include(tv => tv.Tour)
            .SingleOrDefaultAsync(f => f.Id == id);
        return tourVisit == null ? null : _mapper.Map<TourVisitDto>(tourVisit);
    }

    public async Task<TourVisitDto> CreateAsync(TourVisitCreateDto dto)
    {
        var entity = _mapper.Map<TourVisit>(dto);
        entity.VisitDate = DateTime.UtcNow;
        _context.TourVisits.Add(entity);
        await _context.SaveChangesAsync();
        return await _context.TourVisits
            .AsNoTracking()
            .Where(tv => tv.Id == entity.Id)
            .ProjectTo<TourVisitDto>(_mapper.ConfigurationProvider)
            .SingleAsync();
    }

    public async Task<TourVisitDto?> UpdateAsync(Guid id, TourVisitUpdateDto dto)
    {
        var tourVisit = await _context.TourVisits.FindAsync(id);
        if (tourVisit == null) return null;
        
        _mapper.Map(dto, tourVisit);
        await _context.SaveChangesAsync();
        return await _context.TourVisits
            .AsNoTracking()
            .Where(f => f.Id == tourVisit.Id)
            .ProjectTo<TourVisitDto>(_mapper.ConfigurationProvider)
            .SingleAsync();
    }
}