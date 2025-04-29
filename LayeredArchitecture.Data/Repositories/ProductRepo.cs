using LayeredArchitecture.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace LayeredArchitecture.Data.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext _context;
        public ProductRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ProductModel product)
        {
            await _context.Products.AddAsync(product);
        }

        public void Delete(ProductModel product) =>

            _context.Products.Remove(product);



        public async Task<IEnumerable<ProductModel>> GetAllAsync() =>

            await _context.Products.ToListAsync();


        public async Task<ProductModel> GetByIdAsync(int id) => await _context.Products.FindAsync(id);

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new Exception("", e);
            }

        }

        public void Update(ProductModel product)
        {
            _context.Products.Update(product);
        }
    }
}
