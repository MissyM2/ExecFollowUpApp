using Microsoft.EntityFrameworkCore;

namespace EFUApi.Data;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions options): base(options)
  {
    
  }
  public DbSet<Course> Courses {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // data seeding
        modelBuilder.Entity<Course>().HasData(
          new Course {CourseId = 1, Code = "ENG101", CourseNum = 1,Name = "English", Description = "Poetry from 1900"},
          new Course {CourseId = 2, Code = "MATH101", CourseNum = 2, Name = "Math", Description = "Calculus"},
          new Course {CourseId = 3, Code = "PHIL101", CourseNum = 3, Name = "Philosophy", Description = "Logic"},
          new Course {CourseId = 4, Code = "PSYCH101", CourseNum = 4, Name = "Psychology", Description = "Psychology of Women"}
          );
    }

}
