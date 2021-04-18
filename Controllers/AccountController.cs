using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Diagnostics;

using tomiris.ViewModels;
using tomiris.Models;
using tomiris.utils;

namespace tomiris.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext db;
        public AccountController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //TODO Сделать проверку хеша введнного пароля пользователя

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Ищу пользователя в БД и сверяю хеши паролей
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == GeneratePasswordHash(model.Password));

                if (user != null)
                {
                    await Authenticate(model.Email);
                    tomiris.Startup.applicationData["userName"] = User.Identity.Name;

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //TODO Сделать сохранение хеша введнного пароля пользователя

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд + хеш пароля
                    db.Users.Add(new User { Email = model.Email, Password = GeneratePasswordHash(model.Password) });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        // Генерирую хеш

        private static string GeneratePasswordHash(string data)
        {
            HashGenerate hashStringGenerator = new();
            return hashStringGenerator.Compute(data);
        }

        // Даю доступы

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

            Debug.WriteLine($"Аутентифицирован успешно. {userName}");
        }

        // Выход

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            tomiris.Startup.applicationData["userName"] = "null";
            return RedirectToAction("Login", "Account");
        }
    }
}