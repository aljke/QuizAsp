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
            var modell = questions.Where(x => model.Where(y => x.Answer.Where(z => y.Answer.Where(o => o.Id == z.Id)
            .Select(r => new AnswerViewModel {IsCorrect = z.IsCorrect, IsChecked = r.IsChecked})))).ToList();*/

            
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
            Question a;         
            for (int i = 0; i < model.Count; i++)
            {
                for(int j = 0; j < model[i].Answer.Count; j++)
                {
                    var viewAnswer = model[i].Answer[j];
                    /*
                    var dbQuestion = questions.Where(x => x.Id == model[i].Id).First();
                    var dbAnswer = dbQuestion.Answer.Where(x => x.Id == viewAnswer.Id).First();*/
                    a = questions.Where(x => x.Answer.Any(y => y.Id == viewAnswer.Id)).First();

                }
            }         
                 
            return null;
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