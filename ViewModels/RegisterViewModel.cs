using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels
{
    public class RegisterViewModel
    {
        public string Name {  get; set; }   
        public int Age { get; set; }    
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string Confirm_Password { get; set; }
    }
}
