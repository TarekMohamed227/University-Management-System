using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Cryptography;
using WebApplication2.Models;
using WebApplication2.Repositories;


namespace WebApplication2.Controllers
{
    [Authorize (Roles ="Student,Admin")]
    public class StudentController : Controller
    {
        IStudentRepo stdrepo;
        IDeptRepo deptrepo ;
        IStudentCourseRepo stdcrsrepo;

        public StudentController(IStudentRepo _stdrepo, IDeptRepo _deptrepo, IStudentCourseRepo _stdcrsrepo)
        {
            stdrepo = _stdrepo;
            this.deptrepo = _deptrepo;
            stdcrsrepo = _stdcrsrepo;
        }
        public IActionResult Index()
        {
            var eppp = stdrepo.Getall();
            if (eppp == null)
                return Content("Error");
            else
                return View(eppp);        
        }
        public IActionResult Create()
        {
            ViewBag.Deptlist = deptrepo.GetAllDepartments();  
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Student std, IFormFile stdimg)
        {
            string fileext = "";
            if (stdimg != null)
            {
                fileext = stdimg.FileName.Split('.').Last();

                using (var fs = new FileStream($"wwwroot/Images/{std.Id}.{fileext}", FileMode.Create))
                {
                    await stdimg.CopyToAsync(fs);
                    std.stdimg = $"{std.Id}.{fileext}";
                }
            }
            if (std.Id != null && std.Name?.Length > 2)
            {
                stdrepo.Add(std);
                return RedirectToAction("Index");
            }
            else
            return View("Create");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            Student std = stdrepo.Getbyid(id.Value);
            if (std == null)
                return NotFound();
            ViewBag.Deptlist = deptrepo.GetAllDepartments();
            return View(std);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(Student std, IFormFile stdimg,string studimg)
        {
            std.stdimg = "";

            if (stdimg != null)
            {
                string fileext = stdimg.FileName.Split('.').Last();
                using (var fs = new FileStream($"wwwroot/Images/{std.Id}img.{fileext}", FileMode.Create))
                {
                    await stdimg.CopyToAsync(fs);
                }
                std.stdimg = $"{std.Id}img.{fileext}";
            }
            else
            {
                std.stdimg = studimg;
            }
            stdrepo.Update(std);
            return RedirectToAction("Index");     
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var std = stdrepo.Getbyid(id.Value);
            if (std == null)
                return NotFound();
            stdrepo.Delete(std);    
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id) 
        {
          Student std = stdrepo.Getbyid(id);
            var stdcrs = stdcrsrepo.GetStudentCourses(id);
            ViewBag.studentcourses = stdcrs;
            if (std == null)
                return NotFound();
            else
                return PartialView("Details",std);
        }
    }
}
