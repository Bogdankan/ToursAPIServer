using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToursAPI.Models;

public class TourVisit
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public Guid TourId { get; set; }
    public required User User { get; set; }
    public required Tour Tour { get; set; }
    public DateTime VisitDate { get; set; }
    public TimeSpan Duration { get; set; }
}

public class TourVisitConfiguration : IEntityTypeConfiguration<TourVisit>
{
    public void Configure(EntityTypeBuilder<TourVisit> builder)
    {
/*builder.HasKey(tv => tv.Id);

// Зв'язок TourVisit → Tour (багато відвідувань належить одному туру)
builder.HasOne(tv => tv.Tour)
    .WithMany(t => t.TourVisits)
    .HasForeignKey(tv => tv.TourId)
    .OnDelete(DeleteBehavior.Cascade);

// Зв'язок TourVisit → ApplicationUser (багато відвідувань належить одному користувачу)
builder.HasOne(tv => tv.User)
    .WithMany(u => u.TourVisits)
    .HasForeignKey(tv => tv.UserId)
    .OnDelete(DeleteBehavior.Cascade);

// Додаткові налаштування
builder.Property(tv => tv.VisitDate)
    .IsRequired();*/
}
}