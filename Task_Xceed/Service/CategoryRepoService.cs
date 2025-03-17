using Task_Xceed.Data;
using Task_Xceed.Models;
using Task_Xceed.Repository;

namespace Task_Xceed.Service
{
    public class CategoryRepoService : ICategoryRepository
    {
        private ApplicationDbContext _context;

        public CategoryRepoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
