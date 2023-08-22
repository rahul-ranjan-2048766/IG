namespace Instagram.Models
{
    public class Community
    {
        public int Id { get; set; }
        public string Sender { get; set; } = default!;
        public string Message { get; set; } = default!;
        public DateTime DateTime { get; set; }
    }
}


