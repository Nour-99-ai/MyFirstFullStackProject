using FinalProSofra.Models.CommonProp;
using System.ComponentModel.DataAnnotations;

namespace FinalProSofra.Models
{
    public class FormRequest : SharedProp
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public decimal Phone { get; set; }
        public string Subject { get; set; }
    }
}
