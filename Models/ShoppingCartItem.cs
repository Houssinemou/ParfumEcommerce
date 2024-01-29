using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ParfumEcommerce.Models
{
    public class ShoppingCartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Parfum? Parfum { get; set; }
        public int? Quantity { get; set; }

        public ShoppingCart? ShoppingCart { get; set; }
    }
}
