using System.ComponentModel.DataAnnotations;

namespace FinalProSofra.Models
{
    public class User
    {
        [Key]
        public int SenderReciverInfoId { get; set; }

        public string SenderName { get; set; }
        [EmailAddress]
        public string SenderEmail { get; set; }
        public string SenderNumber { get; set; }
        public string SenderCountry { get; set; }
        public bool HideSenderInfo { get; set; }
        public string ReciverName { get; set; }
        public string SenderNotes { get; set; }
        public string ReciverNumber { get; set; }
        public string ReciverLocation { get; set; }
        public string HowHeard { get; set; }
        public DateTime DeliverDate { get; set; }
        public int? CustomerId { get; set; }

    }
}
