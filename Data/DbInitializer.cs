using OnlineBookStore.Models;

namespace OnlineBookStore.Data
{
    public static class DbInitializer
    {
        public static void Seed(BookStoreContext context)
        {
            if (context.Books.Any())
            {
                return;
            }

            var books = new List<Book>
            {
                new Book { Title = "The Pragmatic Programmer", Author = "Andrew Hunt", Category = "Programming", Price = 999, Description = "Classic book on software craftsmanship.", ImageUrl = "https://via.placeholder.com/200x300?text=Book+1" },
                new Book { Title = "Clean Code", Author = "Robert C. Martin", Category = "Programming", Price = 899, Description = "A handbook of agile software craftsmanship.", ImageUrl = "https://via.placeholder.com/200x300?text=Book+2" },
                new Book { Title = "Atomic Habits", Author = "James Clear", Category = "Self Help", Price = 499, Description = "An easy & proven way to build good habits.", ImageUrl = "https://via.placeholder.com/200x300?text=Book+3" },
                new Book { Title = "The Alchemist", Author = "Paulo Coelho", Category = "Fiction", Price = 299, Description = "A fable about following your dream.", ImageUrl = "https://via.placeholder.com/200x300?text=Book+4" }
            };

            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}
