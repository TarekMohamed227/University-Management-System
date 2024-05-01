using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class MyContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source = DESKTOP-MFHMNT5; initial catalog = MVCDB2; integrated security = True; TrustServerCertificate=True ; Encrypt =false");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>().HasKey(c => new { c.Stdid, c.Crsid });
            modelBuilder.Entity<Student>().Property(s => s.stdimg)
            .IsRequired(false);
        }
        public MyContext(DbContextOptions options):base(options) { }
        public MyContext() { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<User> Users { get; set; }  
        public DbSet<Role> Roles { get; set; }  
    }
}
