using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebApplication7.ViewModels;
namespace WebApplication7.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<IdentityUser> _signInManager; 
        public AuthController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm); // Form hatalıysa geri dön

            var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Panel");
            }

            ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre");
            return View(vm); // Hatalı girişte tekrar formu göster
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }




    }
}
