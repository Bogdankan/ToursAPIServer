using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToursAPI.Data;
using ToursAPI.DTOs;

namespace ToursAPI.Services;

public class UserService : IUserService
{
    private readonly AppDBContext _context;
    private readonly IMapper _mapper;

    public UserService(AppDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<UserDto?> GetMeAsync(string userId)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == userId);
        return user is null ? null : _mapper.Map<UserDto>(user);
    }
}