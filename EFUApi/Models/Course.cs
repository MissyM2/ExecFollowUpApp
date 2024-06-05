using System.ComponentModel.DataAnnotations;
using EFUApi.Models.Validations;

namespace EFUApi;

public class Course
{
        public int CourseId { get; set; }

        [Course_EnsureCorrectCode]
        public string? Code { get; set; }

        public int? CourseNum {get; set;}

        public string? Instructor { get; set;}  // added for the purpose of learning versioning

        [Required]
        public string? Name { get; set; }
        
        public string? Description { get; set; }

        public bool ValidateInstructor()
        {
            return !string.IsNullOrEmpty(Instructor);
        }




        
    }