using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Grad_WebApp.ViewModels;
using Grad_WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Grad_WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace Grad_WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly FitnessDbContext _fitnessDbContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly EmailService _emailService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, EmailService emailService, FitnessDbContext fitnessDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _fitnessDbContext = fitnessDbContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password!);
                if (result.Succeeded)
                {
                    // генерация токена для пользователя
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code },
                        protocol: HttpContext.Request.Scheme);
                    await _emailService.SendEmailAsync(model.Email!, "Confirm your account",
                        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

                    return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ResendingConfirmation()
        {
            TempData["Message"] = "Введите E-mail для повторного подтверждения регистрации";
            return View();
        }

        //повторная отправка ссылки для подтверждения
       [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResendingConfirmation(ResendingConfirmationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email!);
                if (user != null && user!.EmailConfirmed)
                {
                    return Content("Регистрация пройдена ранее");
                }
                if (user != null)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code },
                        protocol: HttpContext.Request.Scheme);
                    await _emailService.SendEmailAsync(model.Email!, "Confirm your account",
                        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");
                    return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
                }
                return Content("Пользователя с таким логином не существует");
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                Client? client = await _fitnessDbContext.Clients.FirstOrDefaultAsync(c=>c.Email==user.Email);
                if (client is not null) 
                { 
                    client.UserId = userId;
                    _fitnessDbContext.Update(client);
                    await _fitnessDbContext.SaveChangesAsync();
                }
                else
                {
                    client = new Client() { LastName = user.Email, FirstName = "", Email = user.Email, DateOfBirth = DateTime.MinValue, UserId = userId };
                    _fitnessDbContext.Add(client);
                    await _fitnessDbContext.SaveChangesAsync();
                }   
                await _signInManager.SignInAsync(user, false); // установка куки
                return RedirectToAction("Index", "Timetables");
            }
            else
                return View("Error");
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email!);
                if (user != null)
                {
                    // проверяем, подтвержден ли email
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "Вы не подтвердили свой email");
                        return View(model);
                    }
                }

                var result = await _signInManager.PasswordSignInAsync(model.Email!, model.Password!, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Timetables");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Timetables");
        }

    }
}
