using LayeredArchitecture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArchitecture.Data.Repositories
{
    public interface IProductRepo
    {
        Task<IEnumerable<ProductModel>> GetAllAsync();
        Task<ProductModel> GetByIdAsync(int id);
        Task AddAsync(ProductModel product);
        void Delete(ProductModel product);
        void Update(ProductModel product);
        Task SaveChangesAsync();    
    }
}
