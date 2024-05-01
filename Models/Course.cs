using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int duration { get; set; }
        public ICollection<Department> Departments { get; set; } = new HashSet<Department>();
        public List<StudentCourse> StudentCourses { get; set; }
    }
}
