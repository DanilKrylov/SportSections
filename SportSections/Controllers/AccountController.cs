using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportSections.ViewModels;
using System.Threading.Tasks;

namespace SportSections.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Login, model.Password, true, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "SportSections");
                }
                else
                {
                    ModelState.AddModelError("Password", "Неправильно введено логін або пораль");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "SportSections");
        }
    }
}
