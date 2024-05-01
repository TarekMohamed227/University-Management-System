using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class StudentCourse
    {

        [ForeignKey("Student")]
        public int Stdid { get; set; }
        [ForeignKey("Course")]
        public int Crsid { get; set; }
        public int degree { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
