using ToursAPI.DTOs;

namespace ToursAPI.Services;

public interface IUserService
{
    Task<UserDto?> GetMeAsync(string userId);
}