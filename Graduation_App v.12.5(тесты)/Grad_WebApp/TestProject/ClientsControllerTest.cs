using System;
using Grad_WebApp;
using Grad_WebApp.Controllers;
using Grad_WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Grad_WebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.OutputCaching;
using System.Diagnostics.Metrics;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using Microsoft.Extensions.Options;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Xml;





namespace TestProject
{
  
        [TestClass]
    public sealed class ClientsControllerTest
    {

        [TestMethod]
        public void Index1()
        {
            // Arrange    

            var cache = new Mock<IOutputCacheStore>(MockBehavior.Strict);
            var user_manager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null!, null!, null!, null!, null!, null!, null!, null!);
            string ConnStr = "Host=localhost;Port=5432;Database=FitnessDbContext;Server Compatibility Mode=NoTypeLoading;Pooling=false;Username=tuz;Password=34TuZ078";
            var options = new DbContextOptionsBuilder<FitnessDbContext>().UseNpgsql<FitnessDbContext>(ConnStr).Options;
            var clients = new List<Client>();
            using var db = new FitnessDbContext(options);
            db.Database.OpenConnection();
            clients = db.Clients.OrderBy(c => c.Id).ToList();
            var controller = new ClientsController(db, cache.Object, user_manager.Object);


            // Act
            var result = controller.Index() as Task<ViewResult>;
           
            // Assert   
            Assert.IsNotNull(result);
            db.Database.CloseConnection();
        }

        [TestMethod]
        public void Index2()
        {
            // Arrange    

            var cache = new Mock<IOutputCacheStore>(MockBehavior.Strict);
            var user_manager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null!, null!, null!, null!, null!, null!, null!, null!);
            string ConnStr = "Host=localhost;Port=5432;Database=FitnessDbContext;Server Compatibility Mode=NoTypeLoading;Pooling=false;Username=tuz;Password=34TuZ078";
            var options = new DbContextOptionsBuilder<FitnessDbContext>().UseNpgsql<FitnessDbContext>(ConnStr).Options;
            var clients = new List<Client>();
            using  var db = new FitnessDbContext(options);
            db.Database.OpenConnection();
            clients = db.Clients.OrderBy(c => c.Id).ToList();
            var controller = new ClientsController(db, cache.Object, user_manager.Object);


            // Act
            var result = controller.Index() as Task<ViewResult>;
            var res_model = (List<Client>?)result.Result.Model;

            // Assert   
            Assert.AreEqual(clients.Count, res_model!.Count);
            db.Database.CloseConnection();
        }

        [TestMethod]
        public void Create()
        {
            // Arrange    
            var cache = new Mock<IOutputCacheStore>(MockBehavior.Strict);
            var _userStore = Mock.Of<IUserStore<User>>();
            User testUser = new User() { Email = "Email@list.ru", Year = 1983 };
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            Mock.Get(_userStore).Setup(x => x.CreateAsync(testUser, token)).Returns(Task.FromResult(IdentityResult.Success));
            var user_manager = new Mock<UserManager<User>>(_userStore, null!, null!, null!, null!, null!, null!, null!, null!);
            //Initialize a list of Client objects to back the DbSet with.
            var data = new List<Client>
           {
                new Client
                {
                    FirstName = "Боб",
                    Patronymic = "Степанович",
                    LastName = "Дилан",
                    Email = "StarBux@mail.ru",
                    Phone = "89623850955",
                    DateOfBirth = DateTime.Now,
                    Discount = 0,
                    Сomment = null,
                    UserId = null
                },
           }.AsQueryable();

            Client testClient = new Client{
                FirstName = "Эммануил",
                Patronymic = "Залманович",
                LastName = "Виторган",
                Email = "Star@mail.ru",
                Phone = "89623850909",
                DateOfBirth = DateTime.Now,
                Discount = 33,
                Сomment = null,
                UserId = null
            };
            // Create a mock DbSet.
            var dbSet = new Mock<DbSet<Client>>();
            dbSet.As<IQueryable<Client>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<Client>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<Client>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<Client>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            // Create a mock DbContext.
            var db = new Mock<FitnessDbContext>();
            // Set up the Clients property so it returns the mocked DbSet.
            db.Setup(o => o.Clients).Returns(dbSet.Object);

            /* добавил  конструктор без параметров public FitnessDbContext() { } - иначе ошибка, 
            кроме того, свойство Clients сделал виртуальным public virtual DbSet<Client> Clients { get; set; }!!! */
            var controller = new ClientsController(db.Object, cache.Object, user_manager.Object);


            // Act
            var result = controller.Create(testClient) as Task<IActionResult>;

            // Assert   
            Assert.IsNotNull(result);
        }



    }


}

