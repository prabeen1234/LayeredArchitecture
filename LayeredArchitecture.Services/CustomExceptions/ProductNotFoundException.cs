using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArchitecture.Services.CustomExceptions
{
    public class ProductNotFoundException:Exception
    {

        public ProductNotFoundException(int id) : base($"The product with the productId {id} is not found")
        {

        }
    }
}
