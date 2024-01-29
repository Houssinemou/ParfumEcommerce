using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParfumEcommerce.Models;

namespace ParfumEcommerce.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string connectionString;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public DbSet<Parfum> Parfums { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;
        public DbSet<ShoppingCart> ShoppingCarts { get; set; } = default!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}