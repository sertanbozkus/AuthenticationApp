using AuthenticationApp.Business.Dtos;
using AuthenticationApp.Business.Services;
using AuthenticationApp.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthenticationApp.WebUI.Controllers
{
    // Authentication - Authorization
    // (Kimlik Doğrulama - Yetkilendirme)
    // Bu projede sadece Authentication yapılacak.
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpFormViewModel formData)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.ValidMessage = "Please correct the following errors :";
                return View(formData);
            }

            var signUpDto = new SignUpDto()
            {
                Email = formData.Email.Trim(),
                Password = formData.Password,
                FirstName = formData.FirstName.Trim(),
                LastName = formData.LastName.Trim()
            };

            var result = _userService.AddUser(signUpDto);

            if (!result.IsSucceed)
            {
                ViewBag.ValidMessage = result.Message;
                return View(formData);
            }

            // Viewbag yalnızca return View'de kalırken. TempData redirect için de kullanılabilir.

            TempData["SignUpMessage"]= result.Message;
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        // Await keyword kullanılırsa -> Asenkron bir metot kullanılacak demek -> O zaman geri dönüş tipi async Task<....> şeklinde olmalı.

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel formData)
        {
            if(!ModelState.IsValid)
            {
                return View(formData);
            }

            var signInDto = new SignInDto()
            {
                Email = formData.Email,
                Password = formData.Password
            };

            var userInfo = _userService.SignInUser(signInDto);

            if(userInfo is null)
            {
                ViewBag.ErrorMessage = "The Email or The Password information is wrong.";
                return View(formData);
            }

            // Buraya kadar gelebildiyse kodlar, demek ki kişinin formdan gönderdiği email ve şifre ile DB'deki kayıt eşleşmiş. Bu noktada gerekli bilgileri alıp veritabanından browser'a kadar entity->dto aracılığıyla getirip browserda dosyalarda tutacağım. (Cookie)

            // Oturumda tutacağım her veri -> Claim
            // Bütün cookie bilgileri -> List<Claim>

            var claims = new List<Claim>();

            claims.Add(new Claim("id", userInfo.Id.ToString()));
            claims.Add(new Claim("email", userInfo.Email));
            claims.Add(new Claim("firstName", userInfo.FirstName));
            claims.Add(new Claim("lastName", userInfo.LastName));
            claims.Add(new Claim("fullName", userInfo.FullName));

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            // Claims içerisindeki verilerle bir oturum açılacağı için yukarıdaki identity nesnesini tanımladım.

            var autProperties = new AuthenticationProperties
            {
                
                AllowRefresh = true, // yenilenebiliri oturum.
                ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(48)) // oturum açıldıktan sonra 48 saat geçerli.,
                
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), autProperties);

            // await asenkronize (eşzamansız) yapılan işlemlerin birbirlerini beklemesi için kullanılır. Burada bizim oturum açma işlemimiz hem projemize hem de browsera bağlı olduğu için işlem asenkrondur.

            // Asenkron metotlar geriye "PROMISE" döner.g


             
            return RedirectToAction("Index", "Home");
        }

        public async  Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(); // oturum kapat.

            return RedirectToAction("Index", "Home");
        }

    }
}
