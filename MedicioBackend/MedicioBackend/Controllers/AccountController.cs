using MedicioBackend.Models;
using MedicioBackend.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static MedicioBackend.Helpers.Helper;

namespace MedicioBackend.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public  IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();
           

            AppUser newUser = new AppUser()
            {
                FullName = register.FullName,
                UserName = register.UserName,
                Email = register.Email,

            };

            IdentityResult result = await _userManager.CreateAsync(newUser,register.Password);


            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);


                }
                return View(register);
            }

            await _userManager.AddToRoleAsync(newUser, Roles.Admin.ToString());
            await _signInManager.SignInAsync(newUser, true);
            return RedirectToAction("index", "home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Login(LoginVM login,string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser dbUser=await _userManager.FindByEmailAsync(login.Email);
            if (dbUser == null)
            {
                ModelState.AddModelError("", "email or password wrong");
                return View();
            }

            Microsoft.AspNetCore.Identity.SignInResult result =await  _signInManager.PasswordSignInAsync(dbUser,login.Password,true,true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "your account is lockout");
                return View(login);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "email or password wrong");
                return View(login);
            }
            foreach (var item in await _userManager.GetRolesAsync(dbUser))
            {
                if (item.Contains(Roles.Admin.ToString()))
                {
                    return RedirectToAction("index", "dashboard", new { area = "admin" });
                }
            }

            if (returnUrl==null)
            {
                return RedirectToAction("index", "home");
            }
            return Redirect(returnUrl);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();    
            return RedirectToAction("Index", "Home");
        }

     
        public async Task CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name=item.ToString() });
                }
            }
        }
    }
}
