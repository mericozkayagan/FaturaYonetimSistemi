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
    public class AdminPaymentController : Controller
    {
        PaymentManager pm = new PaymentManager(new EfPaymentDal());
        PaymentValidator validator = new PaymentValidator();
        public IActionResult Index()
        {
            var payments = pm.GetPaymentListWithHouse();
            
            return View(payments);
        }
        [HttpGet]
        public IActionResult AddPayment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddPayment(Payment p)
        {

            ValidationResult result = validator.Validate(p);
            if (result.IsValid)
            {
                p.BillDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                pm.TAdd(p);
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
        public ActionResult EditPayment(int id)
        {
            var paymentValue = pm.GetById(id);
            return View(paymentValue);
        }
        [HttpPost]
        public ActionResult EditPayment(Payment p)
        {
            ValidationResult result = validator.Validate(p);
            if (result.IsValid)
            {
                pm.TUpdate(p);
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
        public IActionResult DeletePayment(int id)
        {
            var payment = pm.GetById(id);
            pm.TDelete(payment);
            return RedirectToAction("Index");
        }
    }
}
