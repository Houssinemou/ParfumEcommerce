using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParfumEcommerce.Data;
using ParfumEcommerce.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParfumEcommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CartController> _logger;

        public CartController(ApplicationDbContext context, ILogger<CartController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(cart => cart.ShoppingCartItems)
                    .ThenInclude(item => item.Parfum)
                .FirstOrDefaultAsync();

            return View(shoppingCart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            try
            {
                var Parfum = await _context.Parfums.FindAsync(productId);
                if (Parfum == null)
                {
                    return NotFound();
                }

                var shoppingCart = await GetOrCreateShoppingCartAsync();
                if (shoppingCart == null)
                {
                    return RedirectToAction("Index");
                }

                var existingCartItem = shoppingCart.ShoppingCartItems.FirstOrDefault(item => item.Parfum != null && item.Parfum.ID == productId);
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += quantity;
                }
                else
                {
                    shoppingCart.ShoppingCartItems.Add(new ShoppingCartItem
                    {
                        Parfum = Parfum,
                     
                    });
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding item to the cart.");
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int itemId)
        {
            var shoppingCart = await GetOrCreateShoppingCartAsync();
            var itemToRemove = shoppingCart.ShoppingCartItems.FirstOrDefault(item => item.Id == itemId);

            if (itemToRemove != null)
            {
                shoppingCart.ShoppingCartItems.Remove(itemToRemove);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        private async Task<ShoppingCart> GetOrCreateShoppingCartAsync()
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(cart => cart.ShoppingCartItems)
                .FirstOrDefaultAsync();

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart();
                _context.ShoppingCarts.Add(shoppingCart);
                await _context.SaveChangesAsync();
            }

            return shoppingCart;
        }
        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            try
            {
                var shoppingCart = await GetOrCreateShoppingCartAsync();
                if (shoppingCart == null)
                {
                    return RedirectToAction("Index");
                }

                shoppingCart.ShoppingCartItems.Clear();
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while clearing the cart.");

                // Handle the error, possibly redirect to an error page
                return RedirectToAction("Error");
            }
        }

    }
}
