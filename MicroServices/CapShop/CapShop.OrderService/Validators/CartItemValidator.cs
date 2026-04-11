using FluentValidation;
using OrderService.DTOs;

namespace OrderService.Validators
{
    public class CartItemValidator : AbstractValidator<CartItemDto>
    {
        public CartItemValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("ProductId must be greater than 0");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }
}
