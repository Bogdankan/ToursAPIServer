using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursAPI.DTOs;
using ToursAPI.Models;
using ToursAPI.Services;

namespace ToursAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IndustriesController : ControllerBase
{
    private readonly IIndustryService _industryService;
    
    public IndustriesController(IIndustryService industryService)
    {
        _industryService = industryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _industryService.GetAllAsync();
        return Ok(companies);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var industry = await _industryService.GetByIdAsync(id);
        if (industry == null) return NotFound();
        return Ok(industry);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create([FromBody] IndustryCreateDto dto)
    {
        var created = await _industryService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update(Guid id, [FromBody] IndustryUpdateDto dto)
    {
        var updated = await _industryService.UpdateAsync(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _industryService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}