using CashinGame.Quiz.Entity.Configurations;
using CashinGame.Quiz.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace CashinGame.Quiz.Entity.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OptionConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
