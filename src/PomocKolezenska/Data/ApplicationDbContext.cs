using PomocKolezenska.Models;
using PomocKolezenska.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace PomocKolezenska.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<Question> Questions { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Question>().ToTable("Questions")
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<User>().ToTable("Users")
            .HasMany(x => x.Questions)
            .WithOne(x => x.Author);
    }
}