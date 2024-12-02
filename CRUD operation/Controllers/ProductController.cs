using CRUD_operation.Data;
using CRUD_operation.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using CRUD_operation.Models;


namespace CRUD_operation.Controllers
{
    public class ProductController : Controller
    {
        private readonly MongoDbContext _context;

        public ProductController(IConfiguration config)
        {
            _context = new MongoDbContext(config);
        }

        // GET: Product
        public IActionResult Index()
        {
            var products = _context.Products.Find(product => true).ToList();
            return View(products);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.Find(c => true).ToList();
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var category = _context.Categories.Find(c => c.Id == product.CategoryId).FirstOrDefault();
                if (category != null)
                {
                    product.CategoryName = category.CategoryName;
                    _context.Products.InsertOne(product);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(product);
        }

        // GET: Product/Edit/{id}
        public IActionResult Edit(string id)
        {
            var product = _context.Products.Find(p => p.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _context.Categories.Find(c => true).ToList();
            return View(product);
        }

        // POST: Product/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Product product)
        {
            if (ModelState.IsValid)
            {
                var category = _context.Categories.Find(c => c.Id == product.CategoryId).FirstOrDefault();
                if (category != null)
                {
                    product.CategoryName = category.CategoryName;
                    var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
                    _context.Products.ReplaceOne(filter, product);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(product);
        }

        // GET: Product/Delete/{id}
        public IActionResult Delete(string id)
        {
            var product = _context.Products.Find(p => p.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            _context.Products.DeleteOne(p => p.Id == id);
            return RedirectToAction(nameof(Index));
        }
    }

}
