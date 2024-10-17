using FinalProSofra.Models;

namespace FinalProSofra.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int ProductId { get; set; }
        public virtual Product? product { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }


    }
}
