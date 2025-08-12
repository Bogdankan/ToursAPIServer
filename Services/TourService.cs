using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToursAPI.Data;
using ToursAPI.DTOs;
using ToursAPI.Models;

namespace ToursAPI.Services;

public class TourService : ITourService
{
    private readonly AppDBContext _context;
    private readonly IMapper _mapper;
    
    public TourService(AppDBContext context,  IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<TourDto>> GetAllAsync()
    {
        var tours = await _context.Tours.ToListAsync();
        return _mapper.Map<IEnumerable<TourDto>>(tours);
    }
    
    public async Task<TourDto?> GetByIdAsync(Guid id)
    {
        var tour = await _context.Tours.FindAsync(id);
        return tour == null ? null : _mapper.Map<TourDto>(tour);
    }
    
    public async Task<TourDto> CreateAsync(TourCreateDto dto)
    {
        var entity = _mapper.Map<Tour>(dto);
        _context.Tours.Add(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<TourDto>(entity);
    }
    
    public async Task<TourDto?> UpdateAsync(Guid id, TourUpdateDto dto)
    {
        var tour = await _context.Tours.FindAsync(id);
        if (tour == null) return null;

        _mapper.Map(dto, tour); 
        await _context.SaveChangesAsync();
        return _mapper.Map<TourDto>(tour);
    }
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await _context.Tours.FindAsync(id);
        if (existing == null) return false;

        _context.Tours.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}