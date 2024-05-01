using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }  
        public int age { get; set; }    

        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Role> Roles { get; set; }=new HashSet<Role>();  
    }
}
