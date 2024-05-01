using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication2.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication2.Repositories
{ 
    public interface IStudentCourseRepo
    {
        StudentCourse GetStudentCoursebyId(int num, int num2);
        void AddStudentCourse(StudentCourse studentCourse);
        List<StudentCourse> GetStudentCourses(int id);
    }
    public class StudentCourseRepo:IStudentCourseRepo
    {
        MyContext db;
        public StudentCourseRepo(MyContext _db)
        {
            db = _db;
        }
        public StudentCourse GetStudentCoursebyId(int num,int num2)
        {
          return db.StudentCourses.FirstOrDefault(a => a.Stdid == num && a.Crsid == num2);
        }

        public void AddStudentCourse (StudentCourse studentCourse)
        {
            db.StudentCourses.Add(studentCourse);   
            db.SaveChanges();
        }

        public List<StudentCourse> GetStudentCourses(int id)
        {
            return db.StudentCourses.Where(s => s.Stdid == id).Include(s => s.Course).ToList();
        }
    }
}
