using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToursAPI.Data;
using ToursAPI.DTOs;
using ToursAPI.Models;

namespace ToursAPI.Services;

public class IndustryService :  IIndustryService
{
    
    private readonly AppDBContext _context;
    private readonly IMapper _mapper;
    
    public IndustryService(AppDBContext context,  IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<IEnumerable<IndustryDto>> GetAllAsync()
    {
        var industries = await _context.Industries.ToListAsync();
        return _mapper.Map<IEnumerable<IndustryDto>>(industries);
    }

    public async Task<IndustryDto?> GetByIdAsync(Guid id)
    {
        var industry = await _context.Industries.FindAsync(id);
        return industry == null ? null : _mapper.Map<IndustryDto>(industry);
    }

    public async Task<IndustryDto> CreateAsync(IndustryCreateDto dto)
    {
        var entity = _mapper.Map<Industry>(dto);
        _context.Industries.Add(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<IndustryDto>(entity);
    }

    public async Task<IndustryDto?> UpdateAsync(Guid id, IndustryUpdateDto dto)
    {
        var industry = await _context.Industries.FindAsync(id);
        if (industry == null) return null;

        _mapper.Map(dto, industry); 
        await _context.SaveChangesAsync();
        return _mapper.Map<IndustryDto>(industry);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await _context.Industries.FindAsync(id);
        if (existing == null) return false;

        _context.Industries.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}