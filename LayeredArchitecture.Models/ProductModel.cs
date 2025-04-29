using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArchitecture.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
  
        public string Name { get; set; }
       
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}
