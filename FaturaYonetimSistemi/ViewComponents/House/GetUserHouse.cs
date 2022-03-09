using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaturaYonetimSistemi.ViewComponents.House
{
    public class GetUserHouse:ViewComponent
    {
        UserManager um = new UserManager(new EfUserDal()); 
        HouseManager hm = new HouseManager(new EfHouseDal());
        public IViewComponentResult Invoke(int id)
        {
            var usermail = User.Identity.Name;
            var value = um.GetList().Where(x => x.Email == usermail);
            if(value is null)
            {
                id = 1;
            }
            else
            {
                var writervalue = um.getLoggedUser(usermail);
                id = writervalue.UserId;
            }            
            
            var values = hm.GetLoggedUserHouse(id);

            return View(values);
        }
    }
}
