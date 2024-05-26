namespace EFUApi.Models.Repositories
;

public static class CourseRepository
{
  private static List<Course> courses = new List<Course>()
    {
        new Course {CourseId = 1, Code = "ENG101", Name = "English", Description = "Poetry from 1900"},
        new Course {CourseId = 2, Code = "MATH101", Name = "Math", Description = "Calculus"},
        new Course {CourseId = 3, Code = "PHIL101", Name = "Philosophy", Description = "Logic"},
        new Course {CourseId = 4, Code = "PSYCH101", Name = "Psychology", Description = "Psychology of Women"}
    };

    public static bool CourseExists(int id)
    {
      return courses.Any(x => x.CourseId == id);
    }

    public static Course? GetCourseById(int id)
    {
      return courses.FirstOrDefault(x => x.CourseId == id);
    }

}
