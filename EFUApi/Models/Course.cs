using System.ComponentModel.DataAnnotations;
using EFUApi.Models.Validations;

namespace EFUApi;

public class Course
{
        public int CourseId { get; set; }

        [Course_EnsureCorrectCode]
        public string? Code { get; set; }

        [Required]
        public string? Name { get; set; }
        
        public string? Description { get; set; }




        
    }