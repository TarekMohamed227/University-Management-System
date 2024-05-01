using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Deptid { get; set; }
        public string Deptname { get; set; }

        public ICollection<Student> Students { get; set; } = new HashSet<Student>();
        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();

    }
}
