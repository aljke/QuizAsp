using QuizAsp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizAsp.Models
{
    public class QuestionViewModel
    {
        public Question question { get; }
        public bool IsChecked;

        public QuestionViewModel()
        {
                   
        }

        public QuestionViewModel(Question question)
        {
            this.question = question;
            IsChecked = false;
        }
    }
}