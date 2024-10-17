using FinalProSofra.data;
using FinalProSofra.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Hala.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;


        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;

        }

       

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Product(int categoryId, decimal? minPrice, decimal? maxPrice)
        {
            // Get all products for the selected category
            var products = _context.Products
                .Where(p => p.CategoryId == categoryId);

            // Apply filtering based on price if provided
            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice.Value);
            }

            // Retrieve the filtered products
            var filteredProducts = products.Include(p => p.Category).ToList();

            // Return to the view with the filtered products
            ViewBag.CategoryId = categoryId; // Keep track of the category ID
            ViewBag.MinPrice = minPrice;      // Pass the minPrice to the view
            ViewBag.MaxPrice = maxPrice;      // Pass the maxPrice to the view

            return View(filteredProducts);
        }

       

       

        public async Task<IActionResult> Cart()
        {


            return View("Cart");
        }


        [HttpPost]

        public IActionResult Contact()
        {
            return View("Contact");
        }

        public IActionResult ProductDetails(int id)
        {
            // Fetch the main product, including its category
            var product = _context.Products
                          .Include(p => p.Category)
                          .FirstOrDefault(p => p.ProductId == id && !p.IsDeleted);

            // Check if the product exists
            if (product == null)
            {
                return NotFound();
            }

            // Fetch related products based on the same category, excluding the current product
            var relatedProducts = _context.Products
                                  .Where(p => p.CategoryId == product.CategoryId && p.ProductId != id && !p.IsDeleted)
                                  .Include(p => p.Category)
                                  .ToList();

            // Pass product details and related products to ViewBag
            ViewBag.Product = product;
            ViewBag.RelatedProducts = relatedProducts;

            return View();
        }


        public IActionResult Shop()
        {
            return View();
        }
      
        public async Task<IActionResult> TrakingOrder()
        {

            var id = HttpContext.Session.GetString("OrderId");
            var Data = _context.Orders.Where(x => x.OrderId == Convert.ToInt64(id)).FirstOrDefault();

            return View(Data);

        }

        
        public IActionResult CheckOut()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
