using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microservice.Models
{
    public class Comment
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Sender { get; set; } = null!;
        public int PostId { get; set; }
        public string Message { get; set; } = null!;
        public DateTime DateTime { get; set; }
    
    }
}
