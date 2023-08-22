namespace Instagram.Models
{
    public class ProfilePic
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string ImageFile { get; set; } = null!;
        public DateTime DateTime { get; set; }
    }
}


