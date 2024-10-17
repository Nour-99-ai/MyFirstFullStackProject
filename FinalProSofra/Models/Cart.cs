using FinalProSofra.Models;

namespace Hala.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProducId { get; set; }
        public Product Product { get; set; }
        public int Qty { get; set; }

    }
}
