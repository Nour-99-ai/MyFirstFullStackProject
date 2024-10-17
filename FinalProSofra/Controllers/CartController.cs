using FinalProSofra.data;
using FinalProSofra.Models;
 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Hala.Controllers
{
    public class CartController : Controller
    {
        public readonly AppDbContext _context;
        public CartController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var orderIdString = HttpContext.Session.GetString("OrderId");
            if (!string.IsNullOrEmpty(orderIdString) && int.TryParse(orderIdString, out int orderId))
            {
                Order Order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
                List<OrderItem> orderItem = _context.orderItems.Where(o => o.OrderId == Order.OrderId).Include(o => o.product).ToList();
                ViewBag.Order = Order;
                return View(orderItem);
            }
            ViewBag.Order = new Order();
            return View(new List<OrderItem>());
        }
        public async Task<IActionResult> AddToCart(int ProductId, int qty = 1)
        {

            Order Order = new Order();

            // Retrieve the order ID from the session
            var orderIdString = HttpContext.Session.GetString("OrderId");

            int orderId;
            if (!string.IsNullOrEmpty(orderIdString) && int.TryParse(orderIdString, out orderId))
            {
                Order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            }
            else
            {
                Order = new Order()
                {
                    Discount = 0,
                    SubPrice = 0,
                    TotalPrice = 0,
                    Status = Status.pending,
                    OrderNo = OrderNumberGenerator.GenerateUniqueOrderNumber()
                };

                await _context.Orders.AddAsync(Order);
                await _context.SaveChangesAsync();

                HttpContext.Session.SetString("OrderId", Order.OrderId.ToString());
            }
            var product = await _context.Products.Where(x => x.ProductId == ProductId).FirstOrDefaultAsync();
            if (product != null)
            {
                OrderItem OrderItem = new OrderItem()
                {
                    Notes = "",
                    OrderId = Order.OrderId,
                    ProductId = ProductId,
                    Order = Order,
                    Price = product.Price,
                    product = product,
                    Quantity = qty
                };

                await _context.orderItems.AddAsync(OrderItem);
                await _context.SaveChangesAsync();

                decimal subtotal = _context.orderItems.Where(o => o.OrderId == Order.OrderId).Sum(item => item.Price * item.Quantity);

                // Calculate discount
                decimal discount = Order.Discount > 0
                    ? (subtotal * Order.Discount / 100)
                    : 0;

                // Calculate total after applying discount
                decimal total = subtotal - discount;

                Order.SubPrice = subtotal;
                Order.TotalPrice = total;
                _context.Orders.Update(Order);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("index");
        }
    }
}
