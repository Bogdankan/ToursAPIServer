using Microsoft.AspNetCore.Mvc;
using ToursAPI.DTOs;
using ToursAPI.Services;

namespace ToursAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TourVisitsController : ControllerBase
{
    private readonly ITourVisitService _tourVisitService;

    public TourVisitsController(ITourVisitService tourVisitService)
    {
        _tourVisitService = tourVisitService;
    }
    
    [HttpGet("by-user/{userId}")]
    public async Task<IActionResult> GetAllToursVisitedByUser(string userId)
    {
        var tourVisits = await _tourVisitService.GetAllToursVisitedByUserAsync(userId);
        return Ok(tourVisits);
    }
    
    [HttpGet("by-tour/{tourId:guid}")]
    public async Task<IActionResult> GetAllUsersVisitedByTour(Guid tourId)
    {
        var tourVisits = await _tourVisitService.GetAllUsersVisitedByTourAsync(tourId);
        return Ok(tourVisits);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var tourVisit = await _tourVisitService.GetByIdAsync(id);
        if (tourVisit == null) return NotFound();
        return Ok(tourVisit);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TourVisitCreateDto dto)
    {
        var created = await _tourVisitService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TourVisitUpdateDto dto)
    {
        var updated = await _tourVisitService.UpdateAsync(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }
}