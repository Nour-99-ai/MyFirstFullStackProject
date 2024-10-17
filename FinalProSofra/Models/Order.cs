using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;

namespace FinalProSofra.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderNo { get; set; }
        public decimal Discount { get; set; }
        public decimal SubPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int? SenderReciverInfoId { get; set; }
        public Status Status { get; set; }
        public virtual User? SenderReciverInfo { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
    public enum Status
    {
        pending = 1,
        Processing = 2,
        Shipped = 3,
        Delivered = 4,
        Cancelled = 5
    }
}
