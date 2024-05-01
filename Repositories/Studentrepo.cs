using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public interface IStudentRepo
    {
        List<Student> Getall();
        Student Getbyid(int id);
        void Add(Student student);
        void Update(Student student);
        void Delete(Student std);
    }
    public class Studentrepo: IStudentRepo
    {
        MyContext db;
        public Studentrepo(MyContext _db) { db = _db; }

        public List<Student> Getall()
        {
            return db.Students.Include(d => d.department).OrderBy(d => d.Dptid).ToList();
        }
        public Student Getbyid (int id)
        {
            return db.Students.Include(a => a.department).FirstOrDefault(a => a.Id == id);
        }
        public void Add(Student student)
        {
            db.Students.Add(student);   
            db.SaveChanges();   
        }
        public void Update(Student student) 
        {
            db.Students.Update(student);
            db.SaveChanges();
        }
        public void Delete(Student std)
        {
            db.Students.Remove(std);
            db.SaveChanges();
        }
       
    }
}
