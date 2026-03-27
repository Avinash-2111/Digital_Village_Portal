using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyVillageApp.Domain.Users;
using MyVillageApp.Models.Account;
using MyVillageApp.Services.Interfaces;

namespace MyVillageApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountController(
            IUserService userService,
            IPasswordHasher<User> passwordHasher)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
        }

        // ===============================
        // REGISTER
        // ===============================

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (await _userService.EmailExistsAsync(model.Email))
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(model);
            }

            if (await _userService.PhoneExistsAsync(model.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "Phone number already exists");
                return View(model);
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                Age = model.Age,
                Role = "User",
                Password=model.Password
            };

          

            await _userService.InsertUserAsync(user);

            return RedirectToAction("RegisterSuccess");
        }

        // ===============================
        // REGISTER SUCCESS PAGE
        // ===============================

        public IActionResult RegisterSuccess()
        {
            return View();
        }

        // ===============================
        // LOGIN
        // ===============================

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userService.GetUserByEmailPasswordAsync(model.Email,model.Password);

            if (user == null)
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();
            }



            // Store session
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserName", user.UserName);
            HttpContext.Session.SetString("UserRole", user.Role);
            return RedirectToAction("Index", "Home");
        }

        // ===============================
        // LOGOUT
        // ===============================

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // ===============================
        // FORGOT PASSWORD (PHONE)
        // ===============================

        public IActionResult ForgotPasswordByPhone()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPasswordByPhone(ForgotPasswordPhoneModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userService.GetUserByPhoneAsync(model.PhoneNumber);

            if (user == null)
            {
                ModelState.AddModelError("PhoneNumber", "Phone number not found");
                return View(model);
            }

            HttpContext.Session.SetString("ResetPhone", model.PhoneNumber);

            return RedirectToAction("ResetPasswordByPhone");
        }

        // ===============================
        // RESET PASSWORD
        // ===============================

        public IActionResult ResetPasswordByPhone()
        {
            var phone = HttpContext.Session.GetString("ResetPhone");

            if (string.IsNullOrEmpty(phone))
                return RedirectToAction("ForgotPasswordByPhone");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordByPhone(ResetPasswordModel model)
        {
            var phone = HttpContext.Session.GetString("ResetPhone");

            if (string.IsNullOrEmpty(phone))
                return RedirectToAction("ForgotPasswordByPhone");

            if (!ModelState.IsValid)
                return View(model);

            var user = await _userService.GetUserByPhoneAsync(phone);

            if (user == null)
                return RedirectToAction("ForgotPasswordByPhone");

            else
                user.Password = model.ConfirmPassword;

            await _userService.UpdateUserAsync(user);

            HttpContext.Session.Remove("ResetPhone");

            TempData["SuccessMessage"] = "Password reset successfully";

            return RedirectToAction("Login");
        }
    }
}