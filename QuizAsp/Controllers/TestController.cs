using QuizAsp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizAsp.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index(int id)
        {
            var model = new QuizModel();
            var currentTestInfo = model.Test.Where(x => x.Id == id).First();

            return View(currentTestInfo);
        }      
    }
}