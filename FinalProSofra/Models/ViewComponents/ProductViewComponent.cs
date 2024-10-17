using FinalProSofra.data;
using Microsoft.AspNetCore.Mvc;

namespace Hala.Models.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private AppDbContext db;
        public ProductViewComponent(AppDbContext _db)
        {
            db = _db;

        }
        public IViewComponentResult Invoke()
        {
            return View(db.Products.ToList());
        }

    }
}
