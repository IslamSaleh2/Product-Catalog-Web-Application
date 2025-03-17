using Task_Xceed.Models;

namespace Task_Xceed.Repository
{
    public interface IProductRepository
    {
        public List<Product> GetProducts();
        public Product GetProductDetails(int id);
        public void InsertProduct(Product std);
        public void UpdateProduct(int id, Product std);
        public void DeleteProduct(int id);

        public List<Product> GetProductsByCategory(int categoryId);

        public List<Product> GetCurrentProducts();
    }
}
