namespace ToursAPI.Models;

public class ToursHistory
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<Tour>? Tours { get; set; }
}