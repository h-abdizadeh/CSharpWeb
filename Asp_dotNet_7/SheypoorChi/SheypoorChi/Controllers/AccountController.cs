using Microsoft.AspNetCore.Mvc;
using SheypoorChi.Core.ViewModels;
using SheypoorChi.Core.Interface;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace ShepoorChi.Controllers;

public class AccountController : Controller
{
    IAdmin _admin;

    //ctor -> tab
    public AccountController(IAdmin admin)
    {
        _admin = admin;
    }

    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "home");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM login)
    {
        if (ModelState.IsValid)
        {
            //get user
            var user = await _admin.GetUser(login);
            if (user is null)
            {
                ModelState.AddModelError("Password", "کاربری با این مشخصات یافت نشد");
                return View(login);
            }

            //login user 
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName)//user mobile
            };

            var identity =
                new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principale = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties()
            {
                IsPersistent = true//remember me 
            };

            //sign in
            await HttpContext.SignInAsync(principale, properties);

            //redirect
            if (user.Role.RoleName is "admin")
            {
                return Redirect("~/admin/panel");
            }
            return Redirect("~/profile/index");

        }
        return View(login);
    }


    public async Task<IActionResult> SignOutUser()
    {
        await HttpContext.SignOutAsync();

        return RedirectToAction("index", "home");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM register)
    {
        if (ModelState.IsValid)
        {
            //check user mobile
            var user = await _admin.GetUser(register.UserName);
            if (user is not null)
            {
                ModelState.AddModelError("UserName",
                    $"شماره {register.UserName} پیش از این ثبت نام کرده است");

                return View(register);
            }

            //register user
            //var result = await _admin.AddUser(register);
            //if (result is true)
            if (await _admin.AddUser(register))
            {
                return RedirectToAction(nameof(Login));
            }

            ModelState.AddModelError("RePassword", "خطا در ثبت نام کاربر");
            return View(register);

        }

        return View(register);
    }
}
