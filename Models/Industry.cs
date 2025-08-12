namespace ToursAPI.Models;

public class Industry
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    
    public ICollection<Company> Companies { get; set; } = new List<Company>();
}