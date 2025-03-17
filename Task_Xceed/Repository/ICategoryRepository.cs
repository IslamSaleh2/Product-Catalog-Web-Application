using Task_Xceed.Models;

namespace Task_Xceed.Repository
{
    public interface ICategoryRepository
    {
        public List<Category> GetCategories();
    }
}
