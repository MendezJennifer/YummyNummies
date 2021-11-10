using YummyNummies.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YummyNummies.Controllers
{
    public class BrowseController : Controller
    {
        //Add DbContext to use database
        private readonly ApplicationDbContext _context;

        //Constructor
        public BrowseController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: /Browse
        public IActionResult Index()
        {
            //Grab and display list of categories from the DB
            var categories = _context.Categories.OrderBy(c => c.Name).ToList();
            return View(categories);
        }

        // GET: /Browse/BrowseByCategory/5
        public IActionResult BrowseByCategory(int id)
        {
            //Retrieve recipes in the selected category
            var products = _context.Recipes.Where(r => r.CategoryId == id)
                .OrderBy(r => r.Name).ToList();

            //Retrieve Category Name (Page Heading)
            var category = _context.Categories.Find(id);
            ViewBag.Category = category.Name;

            return View(products);
        }

    }
}
