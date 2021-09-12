using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjectCamp.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics

        Context context = new Context();

        public ActionResult Index()
        {
            var categoryCount = context.Categories.Count();
            ViewBag.CategoryCount = categoryCount;

            var categoryHeader = context.Headings.Count(x => x.CategoryId == 9);
            ViewBag.CategoryHeader = categoryHeader;

            var writer = context.Writers.Count(x => x.WriterName.Contains("a") || x.WriterName.Contains("A"));
            ViewBag.Writer = writer;

            var maxHeading = context.Categories.Where(c => c.CategoryId == context.Headings
            .GroupBy(x => x.CategoryId).OrderByDescending(x => x.Count()).Select(x => x.Key)
            .FirstOrDefault()).Select(x => x.CategoryName).FirstOrDefault();
            ViewBag.MaxHeading = maxHeading;

            var trueCategories = context.Categories.Count(x => x.CategoryStatus == true);
            var falseCategories = context.Categories.Count(x => x.CategoryStatus == false);
            ViewBag.Status = (trueCategories - falseCategories);


            return View();
        }
    }
}