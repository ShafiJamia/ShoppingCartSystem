using Microsoft.EntityFrameworkCore;
using ShoppingCartSystem.Models;

namespace ShoppingCartSystem.DataAccess
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("BinderShoppingConnectionString"));
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(new Category() { CategoryId = 1, CategoryName = "Healthcare" }, new Category() { CategoryId = 2, CategoryName = "Vegetables" }, new Category() { CategoryId = 3, CategoryName = "Shoes" });

            modelBuilder.Entity<Product>().HasData(new Product() 
                                                   { 
                                                      ProductId = 1,
                                                      Name = "Colgate Toothpaste",
                                                      Description = "Recommended by 9 out of 3.4 doctors",
                                                      Price = 100,
                                                      Review = 3,
                                                      ReviewCount = 4,
                                                      Stock = 10,
                                                      CategoryId = 1
                                                   },
                                                   new Product()
                                                   {
                                                       ProductId = 2,
                                                       Name = "Loreal Shampoo",
                                                       Description = "Great shampoo for losing hair",
                                                       Price = 200,
                                                       Review = 5,
                                                       ReviewCount = 8,
                                                       Stock = 3,
                                                       CategoryId = 1
                                                   },
                                                   new Product()
                                                   {
                                                       ProductId = 3,
                                                       Name = "Addidas Sneakers",
                                                       Description = "Its a shoe, no descriptions needed",
                                                       Price = 1600,
                                                       Review = 3,
                                                       ReviewCount = 45,
                                                       Stock = 16,
                                                       CategoryId = 3
                                                   });

        }
    }
}
