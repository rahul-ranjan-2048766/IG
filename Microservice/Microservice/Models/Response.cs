namespace Microservice.Models
{
    public class Response
    {
        public User User { get; set; } = default!;
        public string Token { get; set; } = default!;
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}


