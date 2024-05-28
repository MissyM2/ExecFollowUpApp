using System.ComponentModel.DataAnnotations;
using EFUWebApp.Models.Validations;

namespace EFUWebApp;

public class Course
{
        public int CourseId { get; set; }

        [Course_EnsureCorrectCode]
        public string? Code { get; set; }

        public int? CourseNum {get; set;}

        [Required]
        public string? Name { get; set; }
        
        public string? Description { get; set; }




        
    }