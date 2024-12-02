using Microsoft.AspNetCore.Mvc;
using CRUD_operation.Models;
using CRUD_operation.Data;
using MongoDB.Driver;

namespace CRUD_operation.Controllers
{
    public class CategoryController : Controller
    {
        private readonly MongoDbContext _context;

        public CategoryController(MongoDbContext context)
        {
            _context = context;
        }

        // Action to display list of categories
        public IActionResult Index()
        {
            var categories = _context.Categories.Find(category => true).ToList();
            return View(categories);
        }

        // Action to display the Create category form
        public IActionResult Create()
        {
            return View();
        }

        // Action to handle the form submission for creating a category
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.InsertOne(category); // Insert category into MongoDB
                return RedirectToAction(nameof(Index)); // Redirect to category list
            }
            return View(category);
        }

        // Action to display the Edit category form
        public IActionResult Edit(string id)
        {
            var category = _context.Categories.Find(c => c.Id == id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // Action to handle the form submission for updating a category
        [HttpPost]
        public IActionResult Edit(string id, Category category)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = _context.Categories.Find(c => c.Id == id).FirstOrDefault();
                if (existingCategory != null)
                {
                    existingCategory.CategoryName = category.CategoryName; // Update the category name
                    _context.Categories.ReplaceOne(c => c.Id == id, existingCategory); // Replace with updated category
                    return RedirectToAction(nameof(Index)); // Redirect to category list
                }
                return NotFound();
            }
            return View(category);
        }

        // Action to handle deleting a category
        public IActionResult Delete(string id)
        {
            var category = _context.Categories.Find(c => c.Id == id).FirstOrDefault();
            if (category != null)
            {
                _context.Categories.DeleteOne(c => c.Id == id); // Delete the category from MongoDB
                return RedirectToAction(nameof(Index)); // Redirect to category list
            }
            return NotFound();
        }
    }
}
