using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace AuthenticationApp.WebUI.Models
{
    public class SignInViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
