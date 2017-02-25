using QuizAsp.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuizAsp.Concrete
{
    public class DbInitializer : DropCreateDatabaseAlways<QuizModel>
    {
        protected override void Seed(QuizModel db)
        {

            Answer answer = new Answer { IsCorrect = true, IdQuestion = 1, Text = "Answer1", Id = 1 };
            var answers = new List<Answer>();
            Test test = new Test { Id = 1, Caption = "Test1", Timer = new TimeSpan(0, 5, 0) };
            Question q = new Question { Text = "A?", Answer = answers, Id = 1, Test = test, TestId = test.Id };
            // db.Answer.Add(Entities.answer);



            //   db.Question.Add();
        }
    }
}