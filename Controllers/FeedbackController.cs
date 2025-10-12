using Microsoft.AspNetCore.Mvc;
using ToursAPI.DTOs;
using ToursAPI.Services;

namespace ToursAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedbackController : ControllerBase
{
    private readonly IFeedbackService _feedbackService;

    public FeedbackController(IFeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
    }
    
    [HttpGet("by-tour/{tourId:guid}")]
    public async Task<IActionResult> GetAllByTour(Guid tourId)
    {
        var feedbacks = await _feedbackService.GetAllByTourAsync(tourId);
        return Ok(feedbacks);
    }
    
    [HttpGet("by-user/{userId}")]
    public async Task<IActionResult> GetAllByUser(string userId)
    {
        var feedbacks = await _feedbackService.GetAllByUserAsync(userId);
        return Ok(feedbacks);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var feedback = await _feedbackService.GetByIdAsync(id);
        if (feedback == null) return NotFound();
        return Ok(feedback);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] FeedbackCreateDto dto)
    {
        var created = await _feedbackService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] FeedbackUpdateDto dto)
    {
        var updated = await _feedbackService.UpdateAsync(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _feedbackService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}