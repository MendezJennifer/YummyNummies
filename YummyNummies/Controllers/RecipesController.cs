using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YummyNummies.Data;
using YummyNummies.Models;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace YummyNummies.Controllers
{
    [Authorize]
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Recipes.Include(r => r.Category).OrderBy(r => r.Name);
            applicationDbContext = (IOrderedQueryable<Recipe>)applicationDbContext.Where(s=>s.UserName.Equals(User.Identity.Name));

            return View(await applicationDbContext.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.Category)
                .FirstOrDefaultAsync(m => m.RecipeId == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecipeId,Name,Rating,UserName,CookTime,Steps,UserId,CategoryId")] Recipe recipe, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                // Save photos (if available)
                if (Photo != null)
                {
                    var photoName = UploadPhoto(Photo);
                    recipe.Photo = photoName;
                }
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", recipe.CategoryId);
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", recipe.CategoryId);
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecipeId,Name,Rating,UserName,CookTime,Steps,UserId,CategoryId")] Recipe recipe, IFormFile Photo, string CurrentPhoto)
        {
            if (id != recipe.RecipeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Save photos (if available)
                    if (Photo != null)
                    {
                        var photoName = UploadPhoto(Photo);
                        recipe.Photo = photoName;
                    }
                    //If photo is not updated, keep current photo
                    else
                    {
                        recipe.Photo = CurrentPhoto;
                    }

                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.RecipeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", recipe.CategoryId);
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.Category)
                .FirstOrDefaultAsync(m => m.RecipeId == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.RecipeId == id);
        }

        //Upload photos
        private static string UploadPhoto(IFormFile Photo)
        {
            //Temporary location of uploaded photo
            var fileLoc = Path.GetTempFileName();

            //Unique file name
            var photoName = Guid.NewGuid() + "-" + Photo.FileName;

            //Dynamic destination path
            var destPath=System.IO.Directory.GetCurrentDirectory()+ "\\wwwroot\\img\\recipes\\" + photoName;

            //Create file copy
            using (var stream = new FileStream(destPath, FileMode.Create))
            {
                Photo.CopyTo(stream);
            }

            return photoName;
        }

        [AllowAnonymous]
        //Search Recipes
        public async Task<IActionResult> Search(string SearchInfo)
        {
            var applicationDbContext = _context.Recipes.Include(r => r.Category).OrderBy(r => r.Name);
            
            //If there is a searchInfo string, filter recipes
            if (!String.IsNullOrEmpty(SearchInfo))
            {
                //Find all the recipes that contain the searchinfo string in the recipe name
                applicationDbContext = (IOrderedQueryable<Recipe>)applicationDbContext.Where(s => s.Name.Contains(SearchInfo)).OrderBy(r => r.Name);
            }

            return View(await applicationDbContext.ToListAsync());
        }
    }
}
