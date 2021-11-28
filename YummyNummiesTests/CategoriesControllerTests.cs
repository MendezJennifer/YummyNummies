using Microsoft.VisualStudio.TestTools.UnitTesting;
using YummyNummies.Controllers;
using YummyNummies.Data;
using YummyNummies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YummyNummiesTests
{
    [TestClass]
    public class CategoriesControllerTests
    {
        //Class level variables
        private ApplicationDbContext _context;
        CategoriesController controller;
        List<Category> categories = new List<Category>();

        //Set up before each test
        [TestInitialize]
        public void TestInitialize()
        {
            // create in-memory db
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            //Create fake categories for the database
            categories.Add(new Category
            {
                CategoryId = 1111,
                Name = "Test: Christmas Cookies",
            });
            categories.Add(new Category
            {
                CategoryId = 2222,
                Name = "Test: Halloween Potions",
            });
            categories.Add(new Category
            {
                CategoryId = 3333,
                Name = "Test: Birthday Cake",
            });

            //Loop through the fake data
            foreach (var category in categories)
            {
                _context.Categories.Add(category);
            }

            //Add fake data to database
            _context.SaveChanges();

            //Instantiate controller (with database)
            controller = new CategoriesController(_context);

        }
            [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
