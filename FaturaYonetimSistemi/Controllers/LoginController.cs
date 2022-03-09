using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace FaturaYonetimSistemi.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(User p)
        {
            Context c = new Context();
            var firstuser = c.Users.SingleOrDefault(x => x.Email == p.Email);
            if (p.Password is null && firstuser is not null)
            {
                int length = 7;
                StringBuilder str_build = new StringBuilder();
                Random random = new Random();
                char letter;
                for (int i = 0; i < length; i++)
                {
                    double flt = random.NextDouble();
                    int shift = Convert.ToInt32(Math.Floor(25 * flt));
                    letter = Convert.ToChar(shift + 65);
                    str_build.Append(letter);
                }
                string generatedPassword = str_build.ToString();
                firstuser.Password = generatedPassword;
                c.Users.Update(firstuser);
                c.SaveChanges();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,p.Email)
                };
                var useridentity = new ClaimsIdentity(claims, "a");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "UserMain");
            }

            else
            {
                var datavalue = c.Users.FirstOrDefault(x => x.Email == p.Email && x.Password == p.Password);

                if (datavalue != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,p.Email)
                };
                    var useridentity = new ClaimsIdentity(claims, "a");
                    ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index", "UserMain");
                }
                else
                {
                    return View();
                }
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AdminLogin(Admin p)
        {
            Context c = new Context();

            var datavalue = c.Admins.FirstOrDefault(x => x.Email == p.Email && x.Password == p.Password);

            if (datavalue != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,p.Email)
                };
                var useridentity = new ClaimsIdentity(claims, "a");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View();
            }


        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
