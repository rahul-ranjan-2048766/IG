namespace Instagram.Models
{
    public class ChatDetails
    {
        public UserAndProfilePic Sender { get; set; } = null!;
        public UserAndProfilePic Receiver { get; set; } = default!;
        public Chat Chat { get; set; } = default!;
    }
}



