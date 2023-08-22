namespace Instagram.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Sender { get; set; } = null!;
        public string Receiver { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime DateTime { get; set; }
    }
}

