using Eshop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Book>? Books { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderItem>? OrderItems { get; set; }
        public DbSet<Address>? Addresses { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Testovaci data
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Malý princ", Author = "Antoine de Saint-Exupéry", Price = 221m},
                new Book { Id = 2, Title = "Hobit", Author = "J. R. R. Tolkien", Price = 376m},
                new Book { Id = 3, Title = "Harry Potter a Kámen mudrců", Author = "J. K. Rowling", Price = 295m}
            );
        }
    }
}
