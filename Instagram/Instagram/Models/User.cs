namespace Instagram.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? Job { get; set; }
        public string? Chronicle { get; set; }
        public string? Firm { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public DateTime DateTimeOfRegistration { get; set; }
        public bool BANNED { get; set; }
    }
}

