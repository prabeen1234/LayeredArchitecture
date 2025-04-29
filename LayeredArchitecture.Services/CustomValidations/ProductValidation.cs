using FluentValidation;
using LayeredArchitecture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArchitecture.Services.CustomValidations
{
    public class ProductValidation:AbstractValidator<ProductModel>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Product name is required.")
                .Length(2, 50)
                .WithMessage("Product name must be between 2 and 50 characters.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product description is required.")
                .Length(10, 200).WithMessage("Product description must be between 10 and 200 characters.");
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Product price is required.")
                .GreaterThan(0).WithMessage("Product price must be greater than 0.");
            
        }
    }
}
