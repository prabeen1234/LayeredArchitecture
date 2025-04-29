using LayeredArchitecture.Models;


namespace LayeredArchitecture.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductModel>> GetAllProducts();
        public Task<ProductModel> GetById(int id);
        public Task CreateProduct(ProductModel model);
        Task DeleteProduct(int id);
        Task UpdateProduct(int id,ProductModel model);
    }
}
