using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            builder.Services.AddTransient<IDeptRepo,DepartmentRepo>();
            builder.Services.AddTransient<IDepartpentCourseRepo, DepartmentCourseRepo>();
            builder.Services.AddTransient<IStudentRepo,Studentrepo>();
            builder.Services.AddTransient<IStudentCourseRepo,StudentCourseRepo>();
            builder.Services.AddDbContext<MyContext>(a =>
            {
                a.UseSqlServer(builder.Configuration.GetConnectionString("con1"));
            }); 

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
