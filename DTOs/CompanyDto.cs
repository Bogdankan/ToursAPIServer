namespace ToursAPI.DTOs;

public class CompanyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid IndustryId { get; set; }
    public string? Url { get; set; }
        
    // Опційно: додати назву індустрії для зручності на клієнті
    public string? IndustryName { get; set; }
    public List<TourDto> Tours { get; set; }
}

public class CompanyCreateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid IndustryId { get; set; }
    public string? Url { get; set; }
}

public class CompanyUpdateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid IndustryId { get; set; }
    public string? Url { get; set; }
}