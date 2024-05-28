using System.ComponentModel.DataAnnotations;

namespace EFUWebApp.Models.Validations
{
  public class Course_EnsureCorrectCodeAttribute : ValidationAttribute
  {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var course = validationContext.ObjectInstance as Course;
            if (course != null && !string.IsNullOrWhiteSpace(course.Name))
            {
              if (course.Name.Equals("English", StringComparison.OrdinalIgnoreCase) && course.Code != "ENG101")
              {
                return new ValidationResult("The only English course we offer is English 101.  Course Code must be ENG101.");
              }
            }

            return ValidationResult.Success;
        }
    }

}
