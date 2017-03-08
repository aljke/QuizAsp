namespace QuizAsp.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    public partial class QuizModel : IdentityDbContext<ApplicationUser>
    {
        public QuizModel()
            : base("name=QuizModel")
        {
            
        }

        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<TestResult> TestResult { get; set; }
       // public virtual DbSet<User> User { get; set; }

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
            
            /*
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.TestResult)
                .WithRequired(e => e.U)
                .WillCascadeOnDelete(false);*/

            modelBuilder.Configurations.Add(new IdentityUserLoginConfiguration());
            modelBuilder.Configurations.Add(new IdentityUserRoleConfiguration());

            Database.SetInitializer<QuizModel>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
