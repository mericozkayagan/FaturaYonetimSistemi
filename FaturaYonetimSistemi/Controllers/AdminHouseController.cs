using BusinessLayer.Concrete;
using BusinessLayer.Validations;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaturaYonetimSistemi.Controllers
{
    public class AdminHouseController : Controller
    {
        HouseManager hm = new HouseManager(new EfHouseDal());
        public IActionResult Index()
        {
            var values = hm.GetList();
            return View(values);
        }
        public IActionResult GetHouseDetail(int id)
        {
            var value = hm.GetById(id);
            return View(value);
        }
        [HttpGet]
        public IActionResult AddHouse()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddHouse(House p)
        {
            HouseValidator validator = new HouseValidator();
            ValidationResult result = validator.Validate(p);
            if (result.IsValid)
            {                
                p.IsEmpty = false;
                hm.TAdd(p);
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
        [HttpGet]
        public IActionResult UpdateHouse(int id)
        {
            var house = hm.GetById(id);
            return View(house);
        }
        [HttpPost]
        public IActionResult UpdateHouse(House p)
        {
            var x = hm.GetList().Where(x => x.HouseId == p.HouseId).SingleOrDefault();
            x.HouseNumber = p.HouseNumber;
            x.Layer = p.Layer;
            x.Type = p.Type;
            x.Block = p.Block;
            hm.TUpdate(x);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteHouse(int id)
        {
            var house = hm.GetById(id);
            //house.IsEmpty = false;
            //hm.TUpdate(house);
            hm.TDelete(house);
            return RedirectToAction("Index");
        }
    }
}
