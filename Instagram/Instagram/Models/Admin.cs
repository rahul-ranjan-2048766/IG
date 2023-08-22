﻿namespace Instagram.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime DateTime { get; set; }
    }
}
