using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microservice.Models
{
    public class Chat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Sender { get; set; } = null!;
        public string Receiver { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime DateTime { get; set; }
    }
}







