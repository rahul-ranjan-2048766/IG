namespace Instagram.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Sender { get; set; } = null!;
        public int PostId { get; set; }
        public string Message { get; set; } = null!;
        public DateTime DateTime { get; set; }
    }
}



