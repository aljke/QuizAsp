using QuizAsp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizAsp.Models
{
    public class UserAnswer
    {
        public int Id { get; set; }

       // public int IdQuestion { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        //public Question Question { get; set; }
        
        public bool IsChecked { get; set; }
    }
}