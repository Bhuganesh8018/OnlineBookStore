using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Data;
using OnlineBookStore.Models;
using System.Text.Json;

namespace OnlineBookStore.Controllers
{
    public class CartController : Controller
    {
        private readonly BookStoreContext _context;
        private const string CartSessionKey = "CartItems";

        public CartController(BookStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cartItems = GetCartItems();
            return View(cartItems);
        }

        public IActionResult AddToCart(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();

            var cartItems = GetCartItems();
            var existing = cartItems.FirstOrDefault(c => c.BookId == id);
            if (existing != null)
            {
                existing.Quantity += 1;
            }
            else
            {
                cartItems.Add(new CartItem
                {
                    BookId = book.Id,
                    Title = book.Title,
                    Price = book.Price,
                    Quantity = 1
                });
            }

            SaveCartItems(cartItems);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Remove(int id)
        {
            var cartItems = GetCartItems();
            var item = cartItems.FirstOrDefault(c => c.BookId == id);
            if (item != null)
            {
                cartItems.Remove(item);
                SaveCartItems(cartItems);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            SaveCartItems(new List<CartItem>());
            return RedirectToAction(nameof(Index));
        }

        private List<CartItem> GetCartItems()
        {
            var json = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(json))
                return new List<CartItem>();

            return JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
        }

        private void SaveCartItems(List<CartItem> items)
        {
            var json = JsonSerializer.Serialize(items);
            HttpContext.Session.SetString(CartSessionKey, json);
        }
    }
}
