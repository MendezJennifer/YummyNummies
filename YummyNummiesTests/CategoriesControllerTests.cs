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
    public class CategoriesControllerTests
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
                Name = "Test: Category"
            };
            _context.Categories.Add(category);

            //Create fake recipes for the database
            recipes.Add(new Recipe
            {
                RecipeId=1111,
                Name="Bad Potion",
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

    }
}
