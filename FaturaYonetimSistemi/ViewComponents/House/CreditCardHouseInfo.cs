using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaturaYonetimSistemi.ViewComponents.House
{
    public class CreditCardHouseInfo:ViewComponent
    {
        PaymentManager pm = new PaymentManager(new EfPaymentDal());
        HouseManager hm = new HouseManager(new EfHouseDal());
        UserManager um = new UserManager(new EfUserDal());

        public IViewComponentResult Invoke(int id)
        {
            var values = pm.GetPaymentListWithHouse(id);

            return View(values);
        }
    }
}
