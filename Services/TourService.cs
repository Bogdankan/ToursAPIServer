using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToursAPI.Data;
using ToursAPI.DTOs;
using ToursAPI.Helpers;
using ToursAPI.Models;

namespace ToursAPI.Services;

public class TourService : ITourService
{
    private readonly AppDBContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    
    private const string StorageFolder = "AssetBundlesStorage";
    private const string DefaultContentType = "application/octet-stream";
    
    public TourService(AppDBContext context,  IMapper mapper,  IWebHostEnvironment env)
    {
        _context = context;
        _mapper = mapper;
        _env = env;
    }
    
    public async Task<IEnumerable<TourDto>> GetAllAsync()
    {
        var tours = await _context.Tours.Include(t => t.Company).ToListAsync();
        return _mapper.Map<IEnumerable<TourDto>>(tours);
    }
    
    public async Task<TourDto?> GetByIdAsync(Guid id)
    {
        var tour = await _context.Tours.Include(t => t.Company).SingleOrDefaultAsync(t => t.Id == id);
        return tour == null ? null : _mapper.Map<TourDto>(tour);
    }

    public async Task<ResourceFile?> GetResourcesAsync(Guid id)
    {
        var tour = await _context.Tours.FindAsync(id);

        if (tour is null) return null;

        // лишаємо тільки ім’я файлу (захист від path traversal)
        var fileName = Path.GetFileName(tour.Resources ?? string.Empty);
        if (string.IsNullOrWhiteSpace(fileName)) return null;

        var fullPath = Path.Combine(_env.ContentRootPath, StorageFolder, fileName);
        if (!System.IO.File.Exists(fullPath)) return null;

        // відкриваємо read-only stream; контролер його закриє після віддачі клієнту
        var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);

        return new ResourceFile(stream, fileName, DefaultContentType);
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