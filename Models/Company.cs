namespace ToursAPI.Models;

public class Company
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public Guid IndustryId { get; set; }
    public string? Url { get; set; }
    
    public Industry Industry { get; set; }
    public ICollection<Tour> Tours { get; set; } = new List<Tour>();
}