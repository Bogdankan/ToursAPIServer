using ToursAPI.Models;

namespace ToursAPI.DTOs;

public class UserDto
{
    public string FullName { get; set; }
    
    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}