using QuizAsp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizAsp.Controllers
{
    public class EditQuizController : Controller
    {
        // GET: EditQuiz
        public ActionResult Index()
        {
            var model = new QuizModel();
            return View();
        }
    }
}