namespace ProductService.Application.CQRS.Validators.CommandValidators.Products
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must(id => id != Guid.Empty).WithMessage("Id cannot be empty.");
        }
    }
}
