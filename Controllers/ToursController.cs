using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursAPI.DTOs;
using ToursAPI.Services;

namespace ToursAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToursController : ControllerBase
{
    private readonly ITourService _tourService;
    private readonly ICompanyService _companyService;
    
    public ToursController(ITourService tourService,  ICompanyService companyService)
    {
        _tourService = tourService;
        _companyService = companyService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _tourService.GetAllAsync();
        return Ok(companies);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var tour = await _tourService.GetByIdAsync(id);
        if (tour == null) return NotFound();
        return Ok(tour);
    }
    
    [HttpGet("{id}/resources")]
    public async Task<IActionResult> GetResourcesById(Guid id)
    {
        var res = await _tourService.GetResourcesAsync(id);
        if (res is null) return NotFound();
        return File(res.Stream, res.ContentType, res.FileName, enableRangeProcessing: true);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create([FromBody] TourCreateDto dto)
    {
        var companyExists = await _companyService.ExistsAsync(dto.CompanyId);
        if (!companyExists)
            return BadRequest($"Company with Id {dto.CompanyId} does not exist");

        var created = await _tourService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update(Guid id, [FromBody] TourUpdateDto dto)
    {
        var updated = await _tourService.UpdateAsync(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _tourService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}