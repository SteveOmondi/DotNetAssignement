
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Customers.Entities;
using OrderManagementSystem.Domain.Orders.Entities;
using OrderManagementSystem.Domain.Promotions.Entities;

namespace OrderManagementSystem.Infrastructure.Persistence
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<SeasonalPromotion> SeasonalPromotions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Order entity
            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(i => i.ProductId);

            // Configure OrderItem entity
            modelBuilder.Entity<OrderItem>()
                .HasKey(i => new { i.ProductId });

            // Configure Customer entity
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Customer>()
                .OwnsOne(c => c.Address); // Value Object Mapping

            base.OnModelCreating(modelBuilder);
        }
    }

}



