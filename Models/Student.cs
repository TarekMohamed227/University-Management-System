using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [StringLength(10,MinimumLength =3,ErrorMessage="Violate string length")]
        public string Name { get; set; }
        public string stdimg { get; set; } = string.Empty;
        [Range(20,60)]
        public int? age { get; set; }
        [ForeignKey("department")]
        public int? Dptid { get; set; }
        public Department department { get; set; }
        public List<StudentCourse> courses { get; set; }
    }
}

