using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using ToursAPI.Helpers;
using ToursAPI.Models;
using ToursAPI.Services;

namespace ToursAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IFeedbackService _feedbackService;
    private readonly ITourVisitService _tourVisitService;
    public UsersController(IUserService userService, IFeedbackService feedbackService,  ITourVisitService tourVisitService)
    {
        _userService = userService;
        _feedbackService = feedbackService;
        _tourVisitService = tourVisitService;
    }
    
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var userId = User.GetUserId(); // див. розширення нижче
        var me = await _userService.GetMeAsync(userId);
        return me is null ? NotFound() : Ok(me);
    }
    
    [HttpGet("me/feedbacks")]
    public async Task<IActionResult> GetMyFeedbacks()
    {
        var userId = User.GetUserId(); // див. розширення нижче
        var feedbacks = await _feedbackService.GetAllByUserAsync(userId);
        return feedbacks is null ? NotFound() : Ok(feedbacks);
    }
    
    [HttpGet("me/tourvisits")]
    public async Task<IActionResult> GetMyTourVisits()
    {
        var userId = User.GetUserId(); // див. розширення нижче
        var tourVisits = await _tourVisitService.GetAllToursVisitedByUserAsync(userId);
        return tourVisits is null ? NotFound() : Ok(tourVisits);
    }
}