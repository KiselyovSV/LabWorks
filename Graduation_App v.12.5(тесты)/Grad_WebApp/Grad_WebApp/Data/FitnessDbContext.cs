using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Grad_WebApp.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Mono.TextTemplating;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data.Common;
using System.Runtime.Intrinsics.X86;
using System;
using System.Configuration;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Grad_WebApp.Data
{
    public class FitnessDbContext: DbContext
    {
        public FitnessDbContext() { }
        public FitnessDbContext(DbContextOptions<FitnessDbContext> options)
        : base(options)
        {
            //Database.EnsureCreated();
        }
        public virtual DbSet<Client> Clients { get; set; }
        public DbSet<Coach> Сoachs { get; set; }
        public DbSet<CoachPhoto> CoachPhotos { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Timetable_Client> Timetable_Clients { get; set; }
        public DbSet<Client_Subscription> Client_Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<User>();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Client_Subscription>(pc =>
        //    {
        //        pc.HasNoKey();
        //    });
        //    modelBuilder.Entity<Timetable_Client>(pc =>
        //    {
        //        pc.HasNoKey();
        //    });

        //}

        /* Введите в консоли диспетчера пакетов(PMC) команду для добавления
         миграции(обратите внимание на необходимость явно указать имя класса
        контекста, так в данном проекте их два):
        PM> Add-Migration InitialCreate -Context 'FitnessDbContext'
        В случае успешного выполнения команды должно быть соответствующее
        сообщение:
        Build started...
        Build succeeded.
        To undo this action, use Remove-Migration.
        8. Выполните обновление базы данных:
        PM> Update-Database -Context 'FitnessDbContext'
        В случае успешного выполнения команды должно быть соответствующее
        сообщение и скрипты для создания базы данных и таблиц:
        Build started...
        Build succeeded.
        Microsoft.EntityFrameworkCore.Database.Command[20101]
        Executed DbCommand (274ms) [Parameters=[], CommandType='Text',
        CommandTimeout='60']
        CREATE DATABASE[CreditContext];*/

    }
}
