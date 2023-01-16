using FluentValidation;

namespace ApiCustomer.Validator
{
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(customer => customer.FullName)
                .NotEmpty()
                .WithMessage("Invalid name, name must not be empty.")
                .Must(IsValidName)
                .WithMessage("Invalid name, name must have only letters.");

            RuleFor(customer => customer.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Invalid email format, expected format: foo@bar.com");

            RuleFor(customer => customer.EmailConfirmation)
                .Equal(customer => customer.Email)
                .WithMessage("Email and EmailConfirmation must be equal");

            RuleFor(customer => customer.Cpf)
                .NotEmpty()
                .WithMessage("Invalid Cpf, expected format: 000.000.000-00");

            RuleFor(customer => customer.Cellphone)
                .NotEmpty()
                .Matches(@"\([0-9]{2}\)[0-9]{5}-[0-9]{4}")
                .WithMessage("Invalid Cellphone, expected format: (00)00000-0000");

            RuleFor(customer => customer.DateOfBirth)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.AddYears(-18))
                .WithMessage("Invalid DateOfBirth: User must be at least 18 years old.");

            RuleFor(customer => customer.EmailSms)
                .NotEmpty();

            RuleFor(customer => customer.Whatsapp)
                .NotEmpty();

            RuleFor(customer => customer.Country)
                .NotEmpty()
                .MinimumLength(4)
                .WithMessage("Invalid Country: Country must have at least 4 letters.");

            RuleFor(customer => customer.City)
                .NotEmpty()
                .WithMessage("Invalid City: City must not be empty.");

            RuleFor(customer => customer.PostalCode)
                .NotEmpty()
                .Matches(@"[0-9]{5}-[0-9]{3}")
                .WithMessage("Invalid PostalCode, expected format: 00000-000");

            RuleFor(customer => customer.Address)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Invalid Address: Address must have at least 3 letters.");

            RuleFor(customer => customer.Num)
                .NotNull();
        }

        private bool IsValidName(string name)
        {
            return name.All(Char.IsLetter);
        }
    }
}
