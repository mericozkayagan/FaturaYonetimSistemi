using BusinessLayer.Concrete;
using BusinessLayer.Validations;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaturaYonetimSistemi.Controllers
{
    public class UserMainController : Controller
    {
        UserManager um = new UserManager(new EfUserDal());
        
        public IActionResult Index()
        {
            var usermail = User.Identity.Name;
            var uservalue = um.getLoggedUser(usermail);
            ViewBag.d = uservalue.UserId;
             
            return View(uservalue);
        }

        [HttpGet]
        public IActionResult EditUser(int id)
        {
           var user = um.GetById(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult EditUser(User p)
        {
            UserValidator validator = new UserValidator();
            var user1 = um.GetList().Where(x => x.UserId == p.UserId).SingleOrDefault();
            p.TCNo = user1.TCNo;
            ValidationResult result = validator.Validate(p);
            string confirmPassword = Request.Form["confirmPassword"];
            if (result.IsValid&& p.Password==confirmPassword)
            {
                um.TUpdate(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}
