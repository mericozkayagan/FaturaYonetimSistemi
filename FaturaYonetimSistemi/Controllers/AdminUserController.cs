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
    public class AdminUserController : Controller
    {
        UserManager um = new UserManager(new EfUserDal());
        public IActionResult Index()
        {
            var users = um.GetList();
            return View(users);
        }
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddUser(User p)
        {
            UserValidator validator = new UserValidator();
            ValidationResult result = validator.Validate(p);
            if (result.IsValid)
            {
                p.IsActive = true;
                um.TAdd(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
            var user = um.GetById(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult UpdateUser(User p)
        {
            UserValidator validator = new UserValidator();
            var user = um.GetList().Where(x => x.UserId == p.UserId).SingleOrDefault();
            p.Password = user.Password;
            p.TCNo = user.TCNo;
            p.PhoneNumber = user.PhoneNumber;
            ValidationResult result = validator.Validate(p);
            if (result.IsValid)
            {
                um.TUpdate(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();
            }
            
        }
        public IActionResult DeleteUser(int id)
        {
            var user = um.GetById(id);
            um.TDelete(user);
            return RedirectToAction("Index");
        }
    }
}
