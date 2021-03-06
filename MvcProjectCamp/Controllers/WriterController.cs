using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjectCamp.Controllers
{
    public class WriterController : Controller
    {

        WriterValidator writervalidator = new WriterValidator();
        WriterManager wm = new WriterManager(new EfWriterDal());

        public ActionResult Index()
        {
            var WriterValues = wm.GetList();
            return View(WriterValues);
        }

        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddWriter(Writer p)
        {
            ValidationResult results = writervalidator.Validate(p);

            if (results.IsValid)
            {
                wm.WriterAdd(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach(var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }

            return View();          
        }

        [HttpGet]
        public ActionResult EditWriter(int id)
        {
            var writervalue = wm.GetById(id);
            return View(writervalue);
        }

        [HttpPost]
        public ActionResult EditWriter(Writer p)
        {
            ValidationResult results = writervalidator.Validate(p);

            if (results.IsValid)
            {
                wm.WriterUpdate(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }
            
            return View();
        }
    }
}