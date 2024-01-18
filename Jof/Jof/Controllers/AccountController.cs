using Jof.Helper;
using Jof.Models;
using Jof.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Jof.Controllers
{
/*    [AutoValidateAntiforgeryToken]
*/    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register() { return View(); }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (User.Identity.IsAuthenticated) { ModelState.AddModelError("", "Siz artiq Login olmusuz"); return View(); }
            if (!vm.Email.Contains("@")) ModelState.AddModelError("Email", "Email formatini duzgwn daxil edin");
            if (!ModelState.IsValid) { return View(vm); }
            AppUser user = new AppUser()
            {
                Name = vm.Name,
                Email = vm.Email,
                Surname = vm.Surname,
                UserName = vm.UserName,
            };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(vm);
            }
            await _signInManager.SignInAsync(user, isPersistent: false);
            await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());
            return RedirectToAction(nameof(Index), "Home");
        }
        public IActionResult Login() { return View(); }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm, string? returnUrl)
        {
            if (User.Identity.IsAuthenticated) { ModelState.AddModelError("", "Siz artiq Login olmusuz"); return View(); }
            if (!ModelState.IsValid) { return View(vm); }
            AppUser user = await _userManager.FindByNameAsync(vm.UsernameOrEmail) ?? await _userManager.FindByEmailAsync(vm.UsernameOrEmail);
            if (user == null) { ModelState.AddModelError(string.Empty, "Username/Email or Password incorrect"); return View(); }
            var result = _signInManager.CheckPasswordSignInAsync(user, vm.Password, true).Result;
            if (result.IsLockedOut) { ModelState.AddModelError(string.Empty, "Try again later"); return View(result); }
            if (!result.Succeeded) { ModelState.AddModelError(string.Empty, "Username/Email or Password incorrect"); }
            await _signInManager.SignInAsync(user, vm.RememberMe);
            return (returnUrl is not null && (returnUrl.Contains("Login"))||returnUrl.Contains("Register")) ? RedirectToAction(nameof(Index), "Home") : (ActionResult)Redirect(returnUrl);
        }
        public IActionResult Logout() { _signInManager.SignOutAsync(); return RedirectToAction(nameof(Index), "Home"); }
        public async Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(UserRole)))
            {
                if (await _roleManager.FindByNameAsync(item.ToString()) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole() { Name = item.ToString() });
                };
            };
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
