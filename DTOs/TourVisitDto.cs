namespace ToursAPI.DTOs;

public class TourVisitDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string TourTitle { get; set; }
    public DateTime VisitDate { get; set; }
    public TimeSpan Duration { get; set; }
}

public class TourVisitCreateDto
{
    public string UserId { get; set; }
    public Guid TourId { get; set; }
    public DateTime VisitDate { get; set; }
}

public class TourVisitUpdateDto
{
    public string UserId { get; set; }
    public Guid TourId { get; set; }
    public TimeSpan Duration { get; set; }
}