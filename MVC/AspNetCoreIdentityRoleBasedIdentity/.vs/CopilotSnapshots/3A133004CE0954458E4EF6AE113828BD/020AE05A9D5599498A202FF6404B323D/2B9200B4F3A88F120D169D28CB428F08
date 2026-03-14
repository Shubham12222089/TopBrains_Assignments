using AspNetCoreIdentityRoleBasedIdentity.Models;
using AspNetCoreIdentityRoleBasedIdentity.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityRoleBasedIdentity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Check if admin key is provided for admin registration
                    string role = "User";
                    if (!string.IsNullOrEmpty(model.AdminKey) && model.AdminKey == "AdminSecret123")
                    {
                        role = "Admin";
                    }

                    await _userManager.AddToRoleAsync(user, role);

                    return RedirectToAction("Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(
                        user,
                        model.Password,
                        false,
                        false
                    );

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Account is locked out");
                    }
                    else if (result.RequiresTwoFactor)
                    {
                        ModelState.AddModelError("", "Two-factor authentication required");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid email or password");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User not found");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please enter valid email and password");
            }

            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

    }
}
