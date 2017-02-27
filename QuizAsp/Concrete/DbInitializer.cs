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
            for (int i = 0; i < 6; i++)
            {
                Answer answer = new Answer { IsCorrect = true, IdQuestion = 1 + i, Text = "Answer1", Id = 1 + i };
                var answers = new List<Answer>();
                Test test = new Test { Id = 1 + i, Caption = "Test1" +i, Timer = new TimeSpan(0, 5, 0) };
                Question question = new Question { Text = "A?", Answer = answers, Id = 1 + i, Test = test, TestId = test.Id };
                db.Answer.Add(answer);
                db.Test.Add(test);
                db.Question.Add(question);
            }
            db.SaveChanges();

            //Seed(db);
            //   db.Question.Add();
        }
    }
}