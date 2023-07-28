using FluentValidation;

namespace Application.Dto
{
    public class CustomerDto : AuditableEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Address)
                .NotEmpty();
        }
    }
}