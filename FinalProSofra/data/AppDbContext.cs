using FinalProSofra.Models;
using FinalProSofra.Models.ViewModels;

//using FinalProSofra.Models.ViewModels;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FinalProSofra.Models;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;


namespace FinalProSofra.data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<ContactUsModel> contactUsModels { get; set; }


        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<CartItem> CartItems { get; set; }



        public DbSet<Product> Products { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call base to ensure Identity is configured

            // Custom configurations for your entities
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
