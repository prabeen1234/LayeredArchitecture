using LayeredArchitecture.Models;
using LayeredArchitecture.Services;
using LayeredArchitecture.Services.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayeredArchitecture.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetById(int id)
        {
            try
            {
                var product = await _productService.GetById(id);
                if (product == null)
                {
                    throw new ProductUnavailableException(id);
                }
                return Ok(product);

            }
            catch (ProductUnavailableException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
          

        }

        [HttpGet]
        public async Task<ActionResult<List<ProductModel>>> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductModel product)
        {
            if (product == null)
            {
                return BadRequest("Product cannot be null");
            }
            await _productService.CreateProduct(product);
            return CreatedAtAction(nameof(GetAllProducts), new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]

        public async Task Delete(int id)
        {
            await _productService.DeleteProduct(id);

        }
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateProduct(int id,ProductModel prod)
        {

            await _productService.UpdateProduct(id, prod);

           
                return Ok("Updated");

           

        }

    }
}
