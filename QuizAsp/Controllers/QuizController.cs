using QuizAsp.Entities;
using QuizAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizAsp.Controllers
{
    public class QuizController : Controller
    {

        // GET: Quiz
        public ActionResult Index(int id = 1)
        {
            // var model = new QuizModel().Test.Where(x => x.Id == id).First();
           // var model = new QuizModel().Test.Where(x => x.Id == id).Select(x => new { x, IsChecked = false}).First();
           var model = new QuizModel()
                .Test.Where(x => x.Id == id).First()
                .Question.Select(x => new QuestionViewModel(x));
            return View(model);
        }

        
        public ActionResult Grade()
        {
            //ViewBag.Count = Request.Form["answer"].Count().ToString();
            //var model = Request.Form.Count.ToString();

            return View();
        }
    }
}