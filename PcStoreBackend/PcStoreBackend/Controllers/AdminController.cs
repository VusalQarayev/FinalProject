using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcStoreBackend.Data;
using PcStoreBackend.Models;

namespace PcStoreBackend.Controllers
{
    [Authorize(Roles = "Admin")] 
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // Add Product GET
        public IActionResult AddProduct()
        {
            return View();
        }

        // Add Product POST
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }

           
            return View(product);
        }


        
        public IActionResult EditProduct(int id)
        {
            var product = _context.Products.Find(id);
            return View(product);
        }

       
        [HttpPost]

        public IActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Dashboard"); 
            }

            
            return View(product);
        }


       
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }
    }
}
