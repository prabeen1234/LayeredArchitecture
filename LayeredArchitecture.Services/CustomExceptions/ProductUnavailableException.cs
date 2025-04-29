using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArchitecture.Services.CustomExceptions
{
    public class ProductUnavailableException:Exception
    {
        public ProductUnavailableException(int id):base($"The product with productID {id} is out of stock")
        {
            
        }

    }
}
