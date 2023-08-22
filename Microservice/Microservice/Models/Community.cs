using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microservice.Models
{
    public class Community
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Sender { get; set; } = default!;
        public string Message { get; set; } = default!;
        public DateTime DateTime { get; set; }
    }
}