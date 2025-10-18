using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFTest.Models
{
    [PrimaryKey(nameof(StudentID), nameof(CourseID))]
    public class StudentCourses
    {
        //Property Navigations

        public int StudentID { get; set; }

        [ForeignKey (nameof(StudentID))]
        public Student? Student { get; set; }
        
        public int CourseID { get; set; }
        
        [ForeignKey(nameof(CourseID))]
        public Course? Course { get; set; }
    }
}
