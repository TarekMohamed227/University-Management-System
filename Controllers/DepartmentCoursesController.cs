using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class DepartmentCoursesController : Controller
    {
        IDepartpentCourseRepo dptcrsrepo;
        IStudentCourseRepo stdcrsrepo;
        public DepartmentCoursesController(IDepartpentCourseRepo _dptcrsrepo, IStudentCourseRepo _stdcrsrepo)
        {
            dptcrsrepo = _dptcrsrepo;
            stdcrsrepo = _stdcrsrepo;
        }
        public IActionResult ShowCourses(int? id)
        {
            Department dept = dptcrsrepo.GetDepartmentById(id.Value);
            return View(dept);
        }
        public IActionResult ManageCourses(int? id)
        {
            Department dept = dptcrsrepo.GetDepartmentById(id.Value);
            var allcourses = dptcrsrepo.GetAllCourses();    
            var CoursesInDept = dept.Courses;
            var CoursesNotInDept =allcourses.Except(CoursesInDept);
            ViewBag.CoursesNotInDept = CoursesNotInDept;
            return View(dept);  
        }
        [HttpPost]
        public IActionResult ManageCourses(int? id,List<int> CoursestoRemove,List<int> CoursestoAdd)
        {
            Department dept = dptcrsrepo.GetDepartmentById(id.Value);
            foreach (var item in CoursestoRemove)
            {
                Course c = dptcrsrepo.GetCourseByID(item);
                dptcrsrepo.RemoveCoursestoDept(dept, c);
            }
            foreach(var item in CoursestoAdd)
            {
                Course c = dptcrsrepo.GetCourseByID(item);
                dptcrsrepo.AddCoursestoDept(dept,c);    
            }
            return RedirectToAction("Index","Department");
        }
        public IActionResult AddDegree(int deptid, int corsid)
        {
            Department dept = dptcrsrepo.GetDepartmentById(deptid);
            Course crs = dptcrsrepo.GetCourseByID(corsid);
            ViewBag.Course = crs;
            return View(dept);
        }
        [HttpPost]
        public IActionResult AddDegree(int corsid ,Dictionary<int,int>degree)
        {
            foreach(var item in degree)
            {
                StudentCourse stdcrs = stdcrsrepo.GetStudentCoursebyId(item.Key,corsid);
                if(stdcrs==null)
                {
                    StudentCourse studentCourse = new StudentCourse()
                    { Stdid = item.Key, Crsid = corsid, degree = item.Value };
                    stdcrsrepo.AddStudentCourse(studentCourse);
                }
                else
                    stdcrs.degree = item.Value;
            }
            return RedirectToAction("index", "Department");
        }
    }
}
