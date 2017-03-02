using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizAsp.Models
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int TestId { get; set; }

        public IList<AnswerViewModel> Answer { get; set; }
    }
}