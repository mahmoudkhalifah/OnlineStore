using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using System;

namespace OnlineStore.Data
{
    public class MainDBContext:DbContext
    {
        public MainDBContext(DbContextOptions<MainDBContext> options): base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; } 
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCart> ProductsCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCart>().HasKey(tc => new { tc.CartId, tc.ProductId });

            modelBuilder.Entity<ProductCart>()
                .HasOne<Product>(sc => sc.Product)
                .WithMany(s => s.ProductsCarts)
                .HasForeignKey(sc => sc.ProductId);

            modelBuilder.Entity<ProductCart>()
                 .HasOne<Cart>(sc => sc.Cart)
                 .WithMany(s => s.ProductsCarts)
                 .HasForeignKey(sc => sc.CartId);


            modelBuilder.Entity<Cart>()
                .HasOne<Customer>(sc => sc.Customer)
                .WithOne(b => b.Cart)
                .HasForeignKey<Cart>(sc => sc.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);
            

            modelBuilder.Entity<Customer>()
                .HasOne<Cart>(sc => sc.Cart)
                .WithOne(b => b.Customer)
                .HasForeignKey<Customer>(sc => sc.CartId)
                .OnDelete(DeleteBehavior.NoAction);






        }

    }

}
