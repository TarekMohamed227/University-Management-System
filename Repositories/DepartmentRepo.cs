using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public interface IDeptRepo
    {
        List<Department> GetAllDepartments();
        Department GetDepartmentById(int id);
        void UpdateDepartment(Department department);
        void DeleteDepartment(Department dpt);
    }
    public class DepartmentRepo:IDeptRepo
    {
        MyContext db ;
        public DepartmentRepo(MyContext _db)
        {
            db= _db;    
        }
        public List<Department> GetAllDepartments()
        {
            return db.Departments.Include(d => d.Courses).Include(s => s.Students).ToList();
        }

        public Department GetDepartmentById (int id)
        {
            return db.Departments.Include(c => c.Students).Include(s => s.Courses).FirstOrDefault(d => d.Deptid == id);

        }
        public void UpdateDepartment(Department department)
        {
            db.Departments.Update(department);
            db.SaveChanges();
        }
        public void DeleteDepartment(Department dpt)
        {
            db.Departments.Remove(dpt);
            db.SaveChanges();
        }

       


    }
}
