using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ToursAPI.Models;

namespace ToursAPI.Data;

public class AppDBContext : IdentityDbContext<User>
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
    
    public DbSet<Tour> Tours => Set<Tour>();
    public DbSet<Industry> Industries => Set<Industry>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Feedback> Feedbacks => Set<Feedback>();
    public DbSet<TourVisit> TourVisits => Set<TourVisit>();
}