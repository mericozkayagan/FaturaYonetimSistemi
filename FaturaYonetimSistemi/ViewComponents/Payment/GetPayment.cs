using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaturaYonetimSistemi.ViewComponents.Payment
{
    public class GetPayment : ViewComponent
    {
        PaymentManager pm = new PaymentManager(new EfPaymentDal());
        public IViewComponentResult Invoke(int id)
        {
            var result = pm.GetPaymentWithHouse(id);
            decimal _sum = decimal.Parse(result.BillSum.ToString()) + decimal.Parse(result.House.Subscription.ToString());
            ViewBag.sum = _sum;
            Value.sum = _sum;
            Value.paymentId = id;
            return View(result);
        }

        public static class Value
        {
            public static decimal sum;
            public static int paymentId;
        }
    }
}
