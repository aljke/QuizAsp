using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using QuizAsp.Entities;

namespace QuizAsp.Controllers
{
    public class StatisticController : Controller
    {

        private QuizModel db = new QuizModel();
        // GET: Statistic
        public ActionResult Index()
        {
            
                ViewBag.Tests = new SelectList(db.Test, "Id", "Caption");
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(int TestId)
        {
            
                ViewBag.Tests = new SelectList(db.Test, "Id", "Caption");
                ViewBag.excellent  =  db.TestResult.Where(x => x.TestId == TestId).Where(z => z.Grade == 5).Count();
                ViewBag.good = db.TestResult.Where(x => x.TestId == TestId).Where(z => z.Grade == 4).Count();
                ViewBag.norm = db.TestResult.Where(x => x.TestId == TestId).Where(z => z.Grade == 3).Count();
                ViewBag.bad = db.TestResult.Where(x => x.TestId == TestId).Where(z => z.Grade == 2).Count();
            
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}