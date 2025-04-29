using LayeredArchitecture.Data;
using LayeredArchitecture.Data.Repositories;
using LayeredArchitecture.Models;
using LayeredArchitecture.Services.CustomExceptions;
using System.Threading.Tasks;
using System.Web.Http;


namespace LayeredArchitecture.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepo _repo;
        public ProductService(ProductRepo repo)
        {
            _repo = repo;
        }

        public async Task CreateProduct(ProductModel model)
        {
            await _repo.AddAsync(model);
            await _repo.SaveChangesAsync();
        }

      

        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            return await _repo.GetAllAsync();
        }
        public async Task<ProductModel> GetById(int id)
        {
            return await _repo.GetByIdAsync(id);


        }

      


        public async Task DeleteProduct(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            _repo.Delete(product);
            await _repo.SaveChangesAsync();
        }

        public async Task UpdateProduct(int id, ProductModel prod)
        {
            var product = await _repo.GetByIdAsync(id);
            product.Name = prod.Name;
            product.Price = prod.Price;
            product.Description = prod.Description;
            product.IsAvailable = prod.IsAvailable;
            _repo.Update(product);
            await _repo.SaveChangesAsync();
        }
    }
}
