using FluentValidation;
using OrderService.DTOs;

namespace OrderService.Validators
{
    public class UpdateCartValidator : AbstractValidator<UpdateCartDto>
    {
        public UpdateCartValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }
}
