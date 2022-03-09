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
    public class UserMessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();

        public ActionResult Inbox()
        {
            var adminMail = User.Identity.Name;
            var messagelist = mm.GetListInbox(adminMail);
            var count = mm.GetListInbox(adminMail).Where(x => x.MessageStatus == false).Count();
            TempData["count"] = count;

            return View(messagelist);
        }
        public ActionResult Sendbox()
        {
            var adminMail = User.Identity.Name;

            var messagelist = mm.GetListSendbox(adminMail);
            return View(messagelist);
        }
        [HttpGet]
        public ActionResult GetInboxMessageDetails(int id)
        {
            var values = mm.GetById(id);
            values.MessageStatus = true;
            mm.TUpdate(values);
            return View(values);
        }

        public ActionResult GetSendboxMessageDetails(int id)
        {
            var values = mm.GetById(id);
            return View(values);
        }
        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Message p)
        {
            var userMail = User.Identity.Name;
            p.SenderMail = userMail;
            ValidationResult result = messagevalidator.Validate(p);
            if (result.IsValid)
            {
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mm.TAdd(p);
                return RedirectToAction("Sendbox");
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

        public PartialViewResult asideBarUser()
        {
            return PartialView();
        }
    }
}
