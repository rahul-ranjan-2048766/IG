namespace Instagram.Models
{
    public class FileUser
    {
        public Post Post { get; set; } = null!;
        public List<UserComment> UserComments { get; set; } = default!;
        public UserAndProfilePic UserAndProfilePic { get; set; } = default!;
    }
}



