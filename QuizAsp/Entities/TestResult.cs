namespace QuizAsp.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestResult")]
    public partial class TestResult
    {
        public int Id { get; set; }

        public int TestId { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public virtual Test Test { get; set; }

        public virtual User User { get; set; }
    }
}
