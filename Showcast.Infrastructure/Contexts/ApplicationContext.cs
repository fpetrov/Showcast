using Microsoft.EntityFrameworkCore;
using Showcast.Core.Entities.Authentication;
using Showcast.Core.Entities.Media;

namespace Showcast.Infrastructure.Contexts;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Movie> Movies { get; set; }
    //public DbSet<Review> Reviews { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     // TODO: Додумать работу с Review: Author нужен??
    //     modelBuilder.Entity<User>()
    //         .HasMany(user => user.LikedMovies)
    //         .WithOne(review => review.);
    // }
}