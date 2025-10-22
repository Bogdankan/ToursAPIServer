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

    public async Task<TourVisitDto?> CreateAsync(TourVisitCreateDto dto)
    {
        var now = DateTime.UtcNow;

        // 1) шукаємо існуючий запис
        var existing = await _context.TourVisits
            .FirstOrDefaultAsync(tv => tv.UserId == dto.UserId && tv.TourId == dto.TourId);

        if (existing != null)
        {
            // 2) оновлюємо дату візиту й повертаємо
            existing.VisitDate = now;
            await _context.SaveChangesAsync();

            return await _context.TourVisits
                .AsNoTracking()
                .Where(tv => tv.Id == existing.Id)
                .ProjectTo<TourVisitDto>(_mapper.ConfigurationProvider)
                .SingleAsync();
        }

        // 3) створюємо новий
        var entity = _mapper.Map<TourVisit>(dto);
        entity.VisitDate = now;
        _context.TourVisits.Add(entity);

        try
        {
            await _context.SaveChangesAsync();

            return await _context.TourVisits
                .AsNoTracking()
                .Where(tv => tv.Id == entity.Id)
                .ProjectTo<TourVisitDto>(_mapper.ConfigurationProvider)
                .SingleAsync();
        }
        catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
        {
            // 4) гонка: хтось створив одночасно → оновлюємо VisitDate вже існуючого і повертаємо його
            var winner = await _context.TourVisits
                .FirstOrDefaultAsync(tv => tv.UserId == dto.UserId && tv.TourId == dto.TourId);

            if (winner == null) return null; // малоймовірно, але захист

            winner.VisitDate = now;
            await _context.SaveChangesAsync();

            return await _context.TourVisits
                .AsNoTracking()
                .Where(tv => tv.Id == winner.Id)
                .ProjectTo<TourVisitDto>(_mapper.ConfigurationProvider)
                .SingleAsync();
        }
    }


    public async Task<TourVisitDto?> UpdateAsync(Guid id, TourVisitUpdateDto dto)
    {
        var tourVisit = await _context.TourVisits.FindAsync(id);
        if (tourVisit == null) return null;
        
        var now = DateTime.UtcNow;
        
        _mapper.Map(dto, tourVisit);
        var duration = now - tourVisit.VisitDate;
        
        duration = new TimeSpan(duration.Hours, duration.Minutes, duration.Seconds);
        
        tourVisit.Duration = duration;
        
        await _context.SaveChangesAsync();
        return await _context.TourVisits
            .AsNoTracking()
            .Where(f => f.Id == tourVisit.Id)
            .ProjectTo<TourVisitDto>(_mapper.ConfigurationProvider)
            .SingleAsync();
    }
    
    private static bool IsUniqueConstraintViolation(DbUpdateException ex)
    {
        // PostgreSQL: 23505 = unique_violation
        if (ex.InnerException is Npgsql.PostgresException pg && pg.SqlState == "23505")
            return true;

        return false;
    }
}