using FinalProSofra.data;
using FinalProSofra.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ProductsController : Controller
{
    private readonly AppDbContext _dbContext;

    public ProductsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // عرض صفحة إضافة منتج
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = _dbContext.Categories.ToList();
        return View();
    }

    // معالجة إضافة المنتج
    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.Categories = _dbContext.Categories.ToList();
        return View(product);
    }

    // عرض المنتجات بناءً على التصنيف
    public IActionResult Index(string categoryName)
    {
        var products = _dbContext.Products.Include(p => p.Category)
                                          .Where(p => p.Category.CategoryName == categoryName)
                                          .ToList();
        if (!products.Any())
        {
            ViewBag.Message = "No products found for this category.";
        }
        return View(products);
    }
}
