namespace ProductService.Application.CQRS.Validators.CommandValidators.Products
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(128).WithMessage("Name cannot exceed 128 characters.");

            When(x => x.Description != null, () =>
            {
                RuleFor(x => x.Description)
                    .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters.");
            });

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Currency)
                .IsInEnum().WithMessage($"Currency is not valid. Use any of {string.Join(", ", Enum.GetNames(typeof(CurrencyEnums)))}");

            RuleFor(x => x.Stock)
                .NotEmpty().WithMessage("Stock is required.")
                .GreaterThan(0).WithMessage("Stock must be greater than 0.");
        }
    }
}