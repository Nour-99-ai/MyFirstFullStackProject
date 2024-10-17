using System.ComponentModel.DataAnnotations;

namespace FinalProSofra.Models
{
    public class CheckoutModel
    {
        [Key]
        public int CheckoutId { get; set; }
        public int OrderId { get; set; }

        public string SenderName { get; set; }

        [EmailAddress]
        public string SenderEmail { get; set; }

        public string SenderAddress { get; set; }

        public string SenderPhone { get; set; }

        public string SenderNotes { get; set; }
        public bool HideSenderInfo { get; set; }
        public DateTime DeliverDate { get; set; }

        public string ReceiverName { get; set; }

        [EmailAddress]

        public string ReceiverAddress { get; set; }

        public string ReceiverPhone { get; set; }
        public string HowHeard { get; set; }

        public List<OrderItem> OrderItems { get; set; } // Use existing OrderItem class

        public decimal Subtotal { get; set; }

        public decimal Shipping { get; set; }

        public decimal Total { get; set; }
    }
}
