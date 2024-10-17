using FinalProSofra.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace FinalProSofra.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public bool IsDeleted { get; set; } // Soft delete flag

        [Required(ErrorMessage = "Enter Product Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Enter Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Enter Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Select a Category")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

      
        public decimal Discount { get; set; }
        public string? Image { get; set; }

        

        [NotMapped]
        public IFormFile? ImageFile { get; set; } // For the uploaded file
    }
}
