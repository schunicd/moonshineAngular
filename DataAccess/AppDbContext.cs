using Microsoft.EntityFrameworkCore;
using TheMoonshineCafe.DataAccess.Models;

namespace TheMoonshineCafe.DataAccess{
    public class AppDbContext : DbContext{
        public AppDbContext(DbContextOptions options) : base(options){}

        public virtual DbSet<Admin> AdminDb { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            var admin = new Admin{
                id = 1,
                name = "Derek",
                email = "schunicd@gmail.com",
                phoneNumber = "1234567890",
                accessLevel = 1
            };

            modelBuilder.Entity<Admin>().HasData(admin);

        }
    }
}