namespace ToursAPI.DTOs;

public class TourDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Resources { get; set; }
    public string CompanyName { get; set; }
}

public class TourCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Resources { get; set; }
    public Guid CompanyId { get; set; }
}

public class TourUpdateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Resources { get; set; }
    public Guid CompanyId { get; set; }
}