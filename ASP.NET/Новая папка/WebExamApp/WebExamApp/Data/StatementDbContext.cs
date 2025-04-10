using Microsoft.EntityFrameworkCore;
using WebExamApp.Models;

namespace WebExamApp.Data
{
    public class StatementDbContext: DbContext
    {
        public StatementDbContext(DbContextOptions<StatementDbContext> options)
        : base(options) 
        {
           /* Database.EnsureCreated();*/   // создаем базу данных при первом обращении
        }
        
        public DbSet<Statement> Statement { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Evaluation> Evaluations { get; set; }  

    }
}
