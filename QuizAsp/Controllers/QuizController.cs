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
        IEnumerable<QuestionViewModel> model;

        // GET: Quiz
        public ActionResult Index(int id = 1)
        {
            // var model = new QuizModel().Test.Where(x => x.Id == id).First();
            // var model = new QuizModel().Test.Where(x => x.Id == id).Select(x => new { x, IsChecked = false}).First();
            var selectedTest = new QuizModel().Test.Where(x => x.Id == id).First();
            ViewBag.Title = selectedTest.Caption;
            ViewBag.Timer = selectedTest.Timer;
            var questions = selectedTest.Question;
            model = questions.Select(x => new QuestionViewModel
            {
                Answer = x.Answer.Select(
                y => new AnswerViewModel
                {
                    IsCorrect = y.IsCorrect,
                    IdQuestion = y.IdQuestion,
                    Question = y.Question,
                    Text = y.Text,
                    IsChecked = false,
                    Id = y.Id
                }).ToList(),
                Id = x.Id,
                TestId = x.TestId,
                Text = x.Text
            });
            TempData["model"] = model;
            return View(model);
        }

        
        public ActionResult Grade()
        {
            model = (IEnumerable<QuestionViewModel>)TempData["model"];
            int grade = model.Count(x => x == (x.Answer.Where(y => y.IsChecked == y.IsCorrect)));
            //var grade = model == null;
           // var m = model.First().Answer.First().IsChecked.ToString();
            ViewBag.grade = grade;
            return View();
        }
    }
}