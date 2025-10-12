using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToursAPI.Models;

public class Feedback
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public Guid TourId { get; set; }
    public required string Comment { get; set; }
    public int Rate { get; set; }
    public DateTime CreatedAt { get; set; }
    public required User User { get; set; }
    public required Tour Tour { get; set; }
}

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        //To do
    }
}