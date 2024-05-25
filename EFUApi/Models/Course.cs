using System.ComponentModel.DataAnnotations;

namespace EFUApi;

public class Course
{
        public int CourseId { get; set; }

        [Required]
        public string? Code { get; set; }

        [Required]
        public string? Name { get; set; }
        
        public string? Description { get; set; }




        
    }