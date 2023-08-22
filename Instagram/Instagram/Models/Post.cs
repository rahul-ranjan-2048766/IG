namespace Instagram.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Sender { get; set; } = default!;
        public string File { get; set; } = default!;
        public DateTime DateTime { get; set; }
    }
}


