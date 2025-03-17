using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task_Xceed.Data;
using Task_Xceed.Models;
using Task_Xceed.Repository;

namespace Task_Xceed.Service
{
    public class ProductRepoService : IProductRepository
    {
        private ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManger;
        public ProductRepoService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManger = userManager;
        }
        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) throw new KeyNotFoundException("Product not found");

            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public List<Product> GetCurrentProducts()
        {
            var currentDate = DateTime.Now;
            var product = _context.Products
                .Include(p => p.Category)
                .Where(p => p.StartDate <= currentDate && p.StartDate.AddDays(p.Duration) >= currentDate)
                .ToList();

            return product;
        }

        public Product GetProductDetails(int id)
        {
            return _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetProducts()
        {
            return _context.Products.Include(item => item.Category).ToList();
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            var currentDate = DateTime.Now;
            return _context.Products.Include(item => item.Category).Where(c => c.CategoryId == categoryId && (c.StartDate <= currentDate && c.StartDate.AddDays(c.Duration) >= currentDate)).ToList();
        }

        public void InsertProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            else
            {
                //product.CreationByUserId = _userManger.GetUserId(User);
                product.CreationDate = DateTime.Now;
                _context.Products.Add(product);
                _context.SaveChanges();
            }
        }

        public void UpdateProduct(int id, Product std)
        {
            Product product = _context.Products.Find(id);
            if (product != null)
            {
                product.Duration = std.Duration;
                product.StartDate = std.StartDate;
                product.Price = std.Price;
                product.Name = std.Name;
                product.CategoryId = std.CategoryId;
                _context.SaveChanges();

            }
            else
            {
                throw new KeyNotFoundException("Product not found");
            }
        }
    }
}
