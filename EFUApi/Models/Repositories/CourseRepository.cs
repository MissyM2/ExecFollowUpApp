using System.Drawing;

namespace EFUApi.Models.Repositories
;

public static class CourseRepository
{
  private static List<Course> courses = new List<Course>()
    {
        new Course {CourseId = 1, Code = "ENG101", CourseNum = 1,Name = "English", Description = "Poetry from 1900"},
        new Course {CourseId = 2, Code = "MATH101", CourseNum = 2, Name = "Math", Description = "Calculus"},
        new Course {CourseId = 3, Code = "PHIL101", CourseNum = 3, Name = "Philosophy", Description = "Logic"},
        new Course {CourseId = 4, Code = "PSYCH101", CourseNum = 4, Name = "Psychology", Description = "Psychology of Women"}
    };

    public static List<Course> GetCourses()
    {
      return courses;
    }

    public static bool CourseExists(int id)
    {
      return courses.Any(x => x.CourseId == id);
    }

    public static Course? GetCourseById(int id)
    {
      return courses.FirstOrDefault(x => x.CourseId == id);
    }

    public static Course? GetCourseByProperties(string? code, int? coursenum, string? name, string? description)
    {
      return courses.FirstOrDefault(x => 
        !string.IsNullOrWhiteSpace(code) &&
        !string.IsNullOrWhiteSpace(x.Code) &&
        x.Code.Equals(code, StringComparison.OrdinalIgnoreCase) &&
        !string.IsNullOrWhiteSpace(name) &&
        !string.IsNullOrWhiteSpace(x.Name) &&
        x.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
        !string.IsNullOrWhiteSpace(description) &&
        !string.IsNullOrWhiteSpace(x.Description) &&
        x.Description.Equals(description, StringComparison.OrdinalIgnoreCase) &&
        coursenum.HasValue &&
        x.CourseNum.HasValue &&
        coursenum.Value == x.CourseNum);
    }

    public static void AddCourse(Course course)
    {
      // because this is an in-memory repo, you have to manually generate the id
      int maxId = courses.Max(x => x.CourseId);
      course.CourseId = maxId + 1;

      courses.Add(course);
    }

    public static void UpdateCourse(Course course)
    {
      var courseToUpdate = courses.First(x => x.CourseId == course.CourseId);
      courseToUpdate.Code = course.Code;
      courseToUpdate.CourseNum = course.CourseNum;
      courseToUpdate.Name = course.Name;
      courseToUpdate.Description = course.Description;

    }

}
