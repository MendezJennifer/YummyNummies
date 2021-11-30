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
    //Recipes Controller: Test Edit (GET)
    [TestClass]
    public class RecipesControllerTests
    {
        //Class level variables
        private ApplicationDbContext _context;
        RecipesController controller;
        List<Recipe> recipes= new List<Recipe>();

        //Set up before each test
        [TestInitialize]
        public void TestInitialize()
        {
            // create in-memory db
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            //Create fake category for the database
            var category = new Category
            {
                CategoryId = 500,
                Name = "Test: Potions"
            };
            _context.Categories.Add(category);

            //Create fake recipes for the database
            recipes.Add(new Recipe
            {
                RecipeId=1111,
                Name="Evil Potion",
                Rating=5,
                UserName="TheWickedWitch",
                CookTime=99,
                Steps= "1.- Collect ingredients. 2.- Drop in cauldron. 3.- Chant Spell 4.- Dance The Macarena 5.- Mix thoroughly 6.- Place curse on chosen individual",
                CategoryId=500,
                Category = category
            });
            recipes.Add(new Recipe
            {
                RecipeId = 2222,
                Name = "Love Potion",
                Rating = 4,
                UserName = "TheGoodWitch",
                CookTime = 66,
                Steps = "1.- Collect ingredients. 2.- Drop in cauldron. 3.- Chant Spell 4.- Dance Mambo No. 5 5.- Mix thoroughly 6.- Drop potion on chosen individual",
                CategoryId = 500,
                Category = category
            });
            recipes.Add(new Recipe
            {
                RecipeId = 3333,
                Name = "Sleep Potion",
                Rating = 3,
                UserName = "TheSleepyWitch",
                CookTime = 77,
                Steps = "1.- Collect ingredients. 2.- Drop in cauldron. 3.- Chant Spell 4.- Dance The Cha Cha Slide 5.- Mix thoroughly 6.- Feed potion to chosen individual",
                CategoryId = 500,
                Category = category
            });

            //Loop through the fake data
            foreach (var recipe in recipes)
            {
                _context.Recipes.Add(recipe);
            }

            //Add fake data to database
            _context.SaveChanges();

            //Instantiate controller (with database)
            controller = new RecipesController(_context);

        }
        // Edit (GET)
        [TestMethod]

        //Test if view loads
        public void LoadsEditView()
        {
            // act 
            var result = (ViewResult)controller.Edit(2222).Result;

            // assert
            Assert.AreEqual("Edit Recipe", result.ViewName);
        }

        [TestMethod]
        //Test if view loads and has data
        public void EditViewDataPopulated()
        {
            // act 
            var result = (ViewResult)controller.Edit(3333).Result;

            // assert
            Assert.IsNotNull(result.ViewData["CategoryId"]);
        }

        [TestMethod]
        //Test if view loads the correct data
        public void EditValidIdLoadsRecipe()
        {
            // act
            var result = (ViewResult)controller.Edit(2222).Result;

            //assert
            Assert.AreEqual(recipes[1], result.Model);
        }



        [TestMethod]
        //Test if error view loads when given inexisting recipeID
        public void EditInValidIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Edit(1000).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        //Test if error view loads when given no recipeID
        public void EditNullIdLoads404()
        {
            // act 
            var result = (ViewResult)controller.Edit(null).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }
    }
}
