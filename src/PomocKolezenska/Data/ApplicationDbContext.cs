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
    public DbSet<QuestionReply> QuestionReplies { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Question>().ToTable("Questions")
            .HasMany(x => x.Replies)
            .WithOne(x => x.Question);

        var r = modelBuilder.Entity<QuestionReply>().ToTable("QuestionReplies");
        r.HasOne(x => x.Question)
            .WithMany(x => x.Replies);
        r.HasOne(x => x.Author)
            .WithMany(x => x.QuestionReplies);
        
        var users = modelBuilder.Entity<User>().ToTable("Users");
        users.HasMany(x => x.Questions)
            .WithOne(x => x.Author);
        users.HasMany(x => x.QuestionReplies)
            .WithOne(x => x.Author);
    }
}