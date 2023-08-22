using Microsoft.EntityFrameworkCore;

namespace Microservice.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public virtual DbSet<User> Users { get; set; } = default!;
        public virtual DbSet<Query> Queries { get; set; } = default!;
        public virtual DbSet<ProfilePic> ProfilePics { get; set; } = null!;
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Community> GetCommunity { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Chat> Chats { get; set; } = null!;
    }
}



