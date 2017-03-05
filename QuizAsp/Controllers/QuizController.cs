using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            
            
           

            return View(model);
        }

        [HttpPost]
        public ActionResult Grade(IList<QuestionViewModel> model)
        {
            int testId = (int)TempData["TestId"];
            var questions = new QuizModel().Question.Where(x => x.TestId == testId).ToList();

            for (int i = 0; i < model.Count; i++)
            {
                model[i].Text = questions.Where(x => x.Id == model[i].Id).First().Text; // get TextQuestion from DB

                // prepare a new viewmodel for showing corrected answers to user
                for (int j = 0; j < model[i].Answer.Count; j++)
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


           
            float grade = 0;
            foreach (var q in model)
            {
                if (q.Answer.All(x => x.IsChecked == x.IsCorrect)) grade++;
            }
            string totalGrade = grade + "/" + model.Count;
            int gradePercent = (int)(grade / model.Count * 100);
            //string gradePercent =  ((int)(grade / model.Count * 100) + "%");
            ViewBag.TotalGrade = totalGrade;
            ViewBag.GradePercent = gradePercent;

            var context = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            var currentUser = context.Users.First(x => x.Id == userId);


            int gradeToDb = 0;
            if (gradePercent >= 90) gradeToDb = 5;
            else if (gradePercent < 90 && gradePercent > 80) gradeToDb = 4;
            else if (gradePercent <= 80 && gradePercent > 70) gradeToDb = 3;
            else gradeToDb = 2;

            var myContext = new QuizModel();
            TestResult res = new TestResult
            {
                Date = DateTime.Now,
                UserId = currentUser.Id,
                Test = myContext.Test.First(x => x.Id == testId),
                Grade = gradeToDb
            };
            myContext.TestResult.Add(res);
            myContext.SaveChanges();

            TempData["model"] = model;

            return View(model);

        }
    }
}