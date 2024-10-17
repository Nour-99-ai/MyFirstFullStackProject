using FinalProSofra.Models;
using System.Collections.Generic;

namespace FinalProSofra.Models.ViewModels
{
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
        public List<Product> RelatedProducts { get; set; } = new List<Product>(); // Initialize to avoid null reference
        public string PageTitle => Product != null ? $"{Product.ProductName} - Details" : "Product Details"; // Example of a dynamic page title
    }
}
