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
            var selectedTest = new QuizModel().Test.Where(x => x.Id == id).First();
            ViewBag.Title = selectedTest.Caption;
            ViewBag.Timer = selectedTest.Timer;
            var questions = selectedTest.Question;
            var model = questions.Select(x => new QuestionViewModel
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
            }).ToList();

            TempData["TestId"] = id;
            return View(model);
        }

        
        [HttpPost]
        public ActionResult Index2(IList<QuestionViewModel> model)
        {
            
            int testId = (int)TempData["TestId"];
            
            var questions = new QuizModel().Question.Where(x => x.TestId == testId).ToList();
            /*
            var mode = from question in questions.AsEnumerable().ToList()
                       from answers in question.Answer
                       from item in model
                       from userAnswer in item.Answer
                       where answers.Id == userAnswer.Id
                       select new AnswerViewModel
                       {
                           Id = answers.Id,
                           IdQuestion = answers.IdQuestion,
                           IsChecked = userAnswer.IsChecked,
                           IsCorrect = answers.IsCorrect,
                           Text = answers.Text,
                           Question = answers.Question
                       };
                  */  
            for (int i = 0; i < model.Count; i++)
            {
                model[i].Text = questions.Where(x => x.Id == model[i].Id).First().Text; // get TextQuestion from DB

                // prepare a new viewmodel for showing corrected answers to user
                for(int j = 0; j < model[i].Answer.Count; j++)
                {
                    var viewAnswer = model[i].Answer[j];
                    var dbAnswer = questions.Where(x => x.Id == model[i].Id)
                        .SelectMany(x => x.Answer)
                        .Where(c => c.Id == viewAnswer.Id)
                        .First();
                    model[i].Answer[j].Text = dbAnswer.Text;
                    model[i].Answer[j].Question = dbAnswer.Question;
                    model[i].Answer[j].IsCorrect = dbAnswer.IsCorrect;
                }
            }

            int grade = 0;
            foreach (var q in model)
            {
                if (q.Answer.All(x => x.IsChecked == x.IsCorrect)) grade++;
            }
            return Content(grade.ToString());
        }
        
        /*
        public ActionResult Grade()
        {
            var model = (IEnumerable<QuestionViewModel>)TempData["model"];
            int grade = model.Count(x => x == (x.Answer.Where(y => y.IsChecked == y.IsCorrect)));
            //var grade = model == null;
           // var m = model.First().Answer.First().IsChecked.ToString();
            ViewBag.grade = grade;
            return View();
        }*/
    }
}