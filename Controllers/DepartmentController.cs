using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Controllers
{
    [Authorize (Roles ="Admin,Instructor")]
    public class DepartmentController : Controller
    {
        IDepartpentCourseRepo dptcrsrepo;
        IDeptRepo dptrepo; 
        public DepartmentController(IDepartpentCourseRepo _dptcrsrepo,IDeptRepo _dptrepo)
        {
            dptcrsrepo= _dptcrsrepo; 
            dptrepo= _dptrepo;  
        }
        public IActionResult Index()
        {
            var Deps = dptrepo.GetAllDepartments();  
            if (Deps == null)
                return Content("Error");
            else
                return View(Deps);
        }
        public IActionResult Create()
        {
            ViewBag.Courses = dptcrsrepo.GetAllCourses();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department dpt,List<int> Coursesindept)
        { 
                foreach(var item in Coursesindept)
                {
                var c = dptcrsrepo.GetCourseByID(item);
                dpt.Courses.Add(c);
                }
            dptcrsrepo.AddDepartmnet(dpt);
            return RedirectToAction("Index","Department");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            Department dept= dptrepo.GetDepartmentById(id.Value);
            if (dept == null)
                return NotFound();
            return View(dept);
        }
        [HttpPost]
        public IActionResult Edit(int id , Department dpt)
        {
            dpt.Deptid = id;
            dptrepo.UpdateDepartment(dpt);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var dpt = dptcrsrepo.GetDepartmentById(id.Value);
            if (dpt == null)
                return NotFound();
            dptrepo.DeleteDepartment(dpt);  
            return RedirectToAction("Index","Department");
        }
    }
    
}
