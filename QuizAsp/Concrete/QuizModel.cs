namespace QuizAsp.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QuizModel : DbContext
    {
        public QuizModel()
            : base("name=QuizModel")
        {
            
        }

        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<TestResult> TestResult { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasMany(e => e.Answer)
                .WithRequired(e => e.Question)
                .HasForeignKey(e => e.IdQuestion);

            modelBuilder.Entity<Test>()
                .HasMany(e => e.TestResult)
                .WithRequired(e => e.Test)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.TestResult)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
