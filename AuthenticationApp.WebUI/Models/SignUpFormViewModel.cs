using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationApp.WebUI.Models
{
    public class SignUpFormViewModel
    {
        [MaxLength(50)]
        [Required]
        public string Email { get; set; }

        [MaxLength(25)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(25)]
        [Required] 
        public string LastName { get; set; }


        [Required]
        [MinLength(5)]
        public string Password { get; set; }

        [Required]
        [MinLength(5)]
        [Compare(nameof(Password))] // Password ile aynı değilse modelstate'i not valid yap.
        public string PasswordConfirm { get; set; }

        

       
    }
}

