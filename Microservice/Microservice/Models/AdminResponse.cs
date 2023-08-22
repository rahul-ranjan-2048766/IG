namespace Microservice.Models
{
    public class AdminResponse
    {
        public Admin Admin { get; set; } = default!;
        public string Token { get; set; } = default!;
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}


