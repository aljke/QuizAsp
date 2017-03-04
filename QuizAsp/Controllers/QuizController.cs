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
            var selectedTest = new QuizModel().Test.Where(x => x.Id == id).First();
            ViewBag.Title = selectedTest.Caption;
            ViewBag.Timer = selectedTest.Timer;
            var questions = selectedTest.Question;
            var model = questions.Select(x => new QuestionViewModel
            {
                Answer = x.Answer.Select(
                y => new UserAnswer
                {
                    IsCorrect = y.IsCorrect,
                    Text = y.Text,
                    IsChecked = false,
                    Id = y.Id
                }).ToList(),
                Id = x.Id,
                TestId = x.TestId,
                Text = x.Text
            }).ToList();

            TempData["TestId"] = id;
            return View(model);
        }

        
        [HttpPost]
        public ActionResult ViewAnswers()
        {
            var model = (IList<QuestionViewModel>)TempData["model"];
            int testId = (int)TempData["TestId"];
            
            var questions = new QuizModel().Question.Where(x => x.TestId == testId).ToList();

            for (int i = 0; i < model.Count; i++)
            {
                model[i].Text = questions.Where(x => x.Id == model[i].Id).First().Text; // get TextQuestion from DB

                // prepare a new viewmodel for showing corrected answers to user
                for(int j = 0; j < model[i].Answer.Count; j++)
                {
                    var userAnswer = model[i].Answer[j];
                    
                    var dbAnswer = questions.Where(x => x.Id == model[i].Id)
                        .SelectMany(x => x.Answer)
                        .Where(c => c.Id == userAnswer.Id)
                        .First();

                    model[i].Answer[j].Text = dbAnswer.Text;
                    model[i].Answer[j].IsCorrect = dbAnswer.IsCorrect;
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Grade(IList<QuestionViewModel> model)
        {
            TempData["model"] = model;
            int grade = 0;
            foreach (var q in model)
            {
                if (q.Answer.All(x => x.IsChecked == x.IsCorrect)) grade++;
            }
            return View(model);
        }
    }
}