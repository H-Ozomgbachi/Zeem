namespace ProductService.Application.CQRS.Validators.CommandValidators.Products
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");

            When(x => !string.IsNullOrEmpty(x.Name), () =>
            {
                RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(128).WithMessage("Name must not exceed 128 characters");
            });

            When(x => !string.IsNullOrEmpty(x.Description), () =>
            {
                RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters");
            });

            When(x => x.Price.HasValue, () =>
            {
                RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");
            });

            When(x => x.Currency != null, () =>
            {
                RuleFor(x => x.Currency)
                .IsInEnum().WithMessage($"Currency is not valid. Use any of {string.Join(", ", Enum.GetNames(typeof(CurrencyEnums)))}");
            });
        }
    }
}
