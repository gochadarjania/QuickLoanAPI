using FluentValidation;
using QuickLoanAPI.Application.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Application.Validators
{
  public class UserRegistrationValidator : AbstractValidator<UserRegistration>
  {
    public UserRegistrationValidator()
    {
      RuleFor(c => c.Email).NotEmpty().NotNull().MaximumLength(25).EmailAddress();
      RuleFor(c => c.Password).NotEmpty().NotNull().MinimumLength(6)
            .Must(x => ContainsSymbol(x) && ContainsNumber(x))
            .WithMessage("{PropertyName} must contain at least one symbol and one number");
      RuleFor(c => c.FirstName).NotEmpty().NotNull();
      RuleFor(c => c.LastName).NotEmpty().NotNull();
      RuleFor(c => c.PersonalNumber).NotEmpty().NotNull().Length(11).Matches("^[0-9]*$").WithMessage("{PropertyName} must contain only digits."); ;
      RuleFor(c => c.DateOfBirth).NotEmpty().NotNull()
            .Must(d => d <= DateTime.Today.AddYears(-18))
            .WithMessage("The person must be over 18 years old.");

    }

    private bool ContainsSymbol(string value)
    {
      return value.Any(c => !char.IsLetterOrDigit(c));
    }

    private bool ContainsNumber(string value)
    {
      return value.Any(char.IsDigit);
    }
  }
}
