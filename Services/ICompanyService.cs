using ToursAPI.DTOs;
using ToursAPI.Models;

namespace ToursAPI.Services;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetAllAsync();
    Task<CompanyDto?> GetByIdAsync(Guid id);
    Task<CompanyDto> CreateAsync(CompanyCreateDto dto);
    Task<CompanyDto?> UpdateAsync(Guid id, CompanyUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}