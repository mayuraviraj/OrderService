using FluentValidation;

namespace OrderService.API.Features.CreateOrder;

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.CustomerName).NotEmpty().MinimumLength(3);
    }
}