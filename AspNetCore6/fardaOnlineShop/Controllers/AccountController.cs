using FardaOnlineShop.ViewModels;
using FardaOnlineShop.Models;
using FardaOnlineShop.Classes;
using Microsoft.AspNetCore.Mvc;
using FardaOnlineShop.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace FardaOnlineShop.Controllers;

public class AccountController : Controller
{
    private readonly DatabaseContext _context;

    public AccountController(DatabaseContext context)
    {
        _context = context;
    }

    public IActionResult Register()
    {
        if (User.Identity.IsAuthenticated)
            return RedirectToAction("index", "profile");

        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Register(Register register)
    {
        if (ModelState.IsValid)
        {
            var mobileArr = register.Mobile.ToArray();
            if (mobileArr[0].ToString() + mobileArr[1].ToString() != "09")
            {
                ModelState.AddModelError("Mobile", "شماره موبایل نامعتبر");
                return View(register);
            }

            var newUser = new User()
            {
                Mobile = register.Mobile,
                Password = new AdminClass().HashPassword(register.Password),
                RoleId = 
                _context.Roles.FirstOrDefault(r => r.Title == "user").Id
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return RedirectToAction("login");
        }
        return View(register);
    }


    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
            return RedirectToAction("index", "profile");

        return View();
    }

    [HttpPost,ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(Login login)
    {
        if (ModelState.IsValid)
        {
            var hashPass = new AdminClass().HashPassword(login.Password);
            var user =
                _context.Users
                .FirstOrDefault(u => u.Mobile == login.Mobile &&
                                    u.Password == hashPass);

            if(user is null)
            {
                ModelState.AddModelError("Password", "کاربری با شماره موبایل و رمزعبور وارد شده یافت نشد");
                return View(login);
            }

            //login user
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Mobile)//user name
            };

            var identity=
                new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principale=new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true,//remember mer
            };

            await HttpContext.SignInAsync(principale, properties);
            return RedirectToAction("index", "profile");

        }

        return View(login);
    }


    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("index", "home");
    }
}
