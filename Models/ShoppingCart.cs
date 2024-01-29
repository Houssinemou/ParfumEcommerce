using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ParfumEcommerce.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParfumEcommerce.Models
{
    public class ShoppingCart
    {
        private readonly ApplicationDbContext _context;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public List<ShoppingCartItem>? ShoppingCartItems { get; set; }

        private ShoppingCart(ApplicationDbContext context)
        {
            _context = context;
        }

        public ShoppingCart()
        {
        }
    }
}
