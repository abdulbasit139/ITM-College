using Microsoft.EntityFrameworkCore;

namespace College.Models
{
    public class CollegeDbContext : DbContext
    {
        public CollegeDbContext(DbContextOptions options) : base(options) 
        { 
            
        }
        public DbSet<CollegeRegistration> CollegeRegistration { get; set; }

        public DbSet<admin> admins { get; set; }
        public DbSet<Feedback> feedback { get; set; }

    }
}
