namespace QuizAsp.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Answer")]
    public partial class Answer
    {
        public int Id { get; set; }

        public int IdQuestion { get; set; }

        [Required]
        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public virtual Question Question { get; set; }
    }
}
