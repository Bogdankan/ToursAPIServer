using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToursAPI.Data;
using ToursAPI.DTOs;
using ToursAPI.Models;

namespace ToursAPI.Services;

public class CompanyService : ICompanyService
{
    
    private readonly AppDBContext _context;
    private readonly IMapper _mapper;
    
    public CompanyService(AppDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<CompanyDto>> GetAllAsync()
    {
        var companies = await _context.Companies.Include(c => c.Industry).Include(c => c.Tours).ToListAsync();
        return _mapper.Map<IEnumerable<CompanyDto>>(companies);
    }

    public async Task<CompanyDto?> GetByIdAsync(Guid id)
    {
        var company = await _context.Companies.Include(c => c.Industry).Include(c => c.Tours).SingleOrDefaultAsync(c => c.Id == id);
        return company == null ? null : _mapper.Map<CompanyDto>(company);
    }

    public async Task<CompanyDto> CreateAsync(CompanyCreateDto dto)
    {
        var entity = _mapper.Map<Company>(dto);
        _context.Companies.Add(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<CompanyDto>(entity);
    }

    public async Task<CompanyDto?> UpdateAsync(Guid id, CompanyUpdateDto  dto)
    {
        var company = await _context.Companies.FindAsync(id);
        if (company == null) return null;

        _mapper.Map(dto, company); 
        await _context.SaveChangesAsync();
        return _mapper.Map<CompanyDto>(company);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await _context.Companies.FindAsync(id);
        if (existing == null) return false;

        _context.Companies.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Companies.AnyAsync(c => c.Id == id);
    }
}