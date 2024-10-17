using FinalProSofra.data;
using FinalProSofra.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace FinalProSofra.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly AppDbContext _context;

        public CheckOutController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            var orderIdString = HttpContext.Session.GetString("OrderId");
            if (string.IsNullOrEmpty(orderIdString))
            {
                return RedirectToAction("Index");
            }

            if (!int.TryParse(orderIdString, out int orderId))
            {
                return RedirectToAction("Index");
            }

            var order = _context.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.OrderId == orderId);
            if (order == null || order.OrderItems == null || !order.OrderItems.Any())
            {
                return RedirectToAction("Index");
            }

            var checkoutModel = new CheckoutModel
            {
                OrderId = orderId, // Set OrderId
                SenderName = "Default Name",
                SenderEmail = "example@example.com",
                SenderPhone = "123456789",
                SenderAddress = "Default Address",
                SenderNotes = "Some notes",
                HideSenderInfo = false,
                DeliverDate = DateTime.Now.AddDays(1), // Example
                ReceiverName = "Receiver Name",
                ReceiverAddress = "Receiver Address",
                ReceiverPhone = "Receiver Phone",
                HowHeard = "Friend",
                OrderItems = order.OrderItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList(),
                Subtotal = order.SubPrice,
                Shipping = 50, // Example static value
                Total = order.TotalPrice
            };

            return View(checkoutModel);
        }


        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CheckoutModel model)
        {
            if (ModelState.IsValid)
            {
                var Order = _context.Orders.FirstOrDefault(o => o.OrderId == model.OrderId);
                Order.Status = Status.Processing;

                User sender = new User()
                {
                    DeliverDate = model.DeliverDate,
                    HideSenderInfo = model.HideSenderInfo,
                    HowHeard = model.HowHeard,
                    ReciverLocation = model.ReceiverAddress,
                    ReciverName = model.ReceiverName,
                    SenderNotes = model.SenderNotes,
                    ReciverNumber = model.ReceiverPhone,
                    SenderCountry = model.SenderAddress,
                    SenderEmail = model.SenderEmail,
                    SenderName = model.SenderName,
                    SenderNumber = model.SenderPhone

                };
                _context.Users.Add(sender);
                _context.SaveChanges();

                Order.SenderReciverInfo = sender;
                Order.SenderReciverInfoId = sender.SenderReciverInfoId;
                _context.Orders.Update(Order);
                _context.SaveChanges();
                TempData["CheckoutModel"] = JsonConvert.SerializeObject(model);

                return RedirectToAction("Index", "PayPal");
            }

            // If model state is not valid, return to Checkout view with the model to show validation errors
            return View("Checkout", model);
        }

        public IActionResult OrderConfirmation()
        {

            var checkoutModelJson = TempData["CheckoutModel"] as string;
            var checkoutModel = JsonConvert.DeserializeObject<CheckoutModel>(checkoutModelJson);

            // Pass the model to the OrderConfirmation view
            return View(checkoutModel);
        }

        private bool ValidateCheckoutInputs(string senderName, string senderEmail, int senderNumber, string receiverName, string receiverEmail, int receiverNumber, string receiverLocation)
        {
            return !string.IsNullOrWhiteSpace(senderName) &&
                   !string.IsNullOrWhiteSpace(senderEmail) &&
                   senderNumber > 0 &&
                   !string.IsNullOrWhiteSpace(receiverName) &&
                   !string.IsNullOrWhiteSpace(receiverEmail) &&
                   receiverNumber > 0 &&
                   !string.IsNullOrWhiteSpace(receiverLocation);
        }
    }
}
