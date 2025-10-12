namespace ToursAPI.DTOs;

public class FeedbackDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public Guid TourId { get; set; }
    public string TourTitle { get; set; }
    public string Username { get; set; }
    public string Comment { get; set; }
    public int Rate { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class FeedbackCreateDto
{
    public string UserId { get; set; }
    public Guid TourId { get; set; }
    public string Comment { get; set; }
    public int Rate { get; set; }
}

public class FeedbackUpdateDto
{
    public string UserId { get; set; }
    public Guid TourId { get; set; }
    public string Comment { get; set; }
    public int Rate { get; set; }
}