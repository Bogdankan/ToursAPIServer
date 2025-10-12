using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;

namespace ToursAPI.Models;

public class User : IdentityUser
{
    public required string FullName { get; set; }
    
    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //To do
    }
}