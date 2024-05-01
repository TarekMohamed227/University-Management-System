using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public interface IDepartpentCourseRepo
    {
        Department GetDepartmentById(int id);
        List<Course> GetAllCourses();
        Course GetCourseByID(int id);
        void AddDepartmnet(Department department);
        void AddCoursestoDept(Department department, Course course);
        void RemoveCoursestoDept(Department department, Course course);
    }
    public class DepartmentCourseRepo:IDepartpentCourseRepo
    {
        MyContext db;
        public DepartmentCourseRepo(MyContext _db)
        {
            db = _db;
        }

        public Department GetDepartmentById (int id)
        {
            return db.Departments.Include(c => c.Students).Include(s => s.Courses).FirstOrDefault(d => d.Deptid == id);
        }
        public List<Course> GetAllCourses()
        {
            return db.Courses.ToList(); 
        }
        public Course GetCourseByID (int id)
        {
            return db.Courses.FirstOrDefault(c => c.Id == id);
        }
        public void AddDepartmnet(Department department)
        {
            db.Departments.Add(department); 
            db.SaveChanges();
        }
     
        public void AddCoursestoDept (Department department,Course course)
        {
            department.Courses.Add(course); 
            db.SaveChanges();
        }
        public void RemoveCoursestoDept(Department department, Course course)
        {
            department.Courses.Remove(course);
            db.SaveChanges();
        }



    }
}
