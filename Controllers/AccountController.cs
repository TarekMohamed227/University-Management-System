using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User() { Name=model.Name, Email=model.Email,Password=model.Password , age=model.Age};
               MyContext db = new MyContext();
                db.Users.Add(user);
                db.SaveChanges();
                var role = db.Roles.FirstOrDefault(a=>a.Name == "Student");
                user.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Login");

            }
            return View(model);
        }
        

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Login(LoginViewModel model)
        {
            MyContext db    = new MyContext();
           var res = db.Users.Include(a=>a.Roles).FirstOrDefault(a => a.Email == model.Email && a.Password == model.Password) ; 
            if(res == null)
            {
                ModelState.AddModelError("", "Username and Password are invalid");
                return View();
            }
            Claim c1 = new Claim(ClaimTypes.Name, res.Name);
            Claim c2 = new Claim (ClaimTypes.Email,res.Email);
            List<Claim> Roleclaims = new List<Claim>(); 
            foreach(var item in res.Roles)
            {
                Roleclaims.Add(new Claim(ClaimTypes.Role, item.Name));
            }

            ClaimsIdentity ci = new ClaimsIdentity("Cookies");
            ci.AddClaim(c1);
            ci.AddClaim(c2);
            foreach(var item in Roleclaims)
            {
                ci.AddClaim(item);
            }
            ClaimsPrincipal cp = new ClaimsPrincipal();
            cp.AddIdentity(ci); 
          await  HttpContext.SignInAsync(cp);
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
         await   HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");        
        }
    }
}
