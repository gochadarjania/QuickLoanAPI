using FluentValidation;
using QuickLoanAPI.Application.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Application.Validators
{
  public class UserLoginValidator : AbstractValidator<LoginModel>
  {
    public UserLoginValidator()
    {
      RuleFor(c => c.Email).NotEmpty().NotNull().MaximumLength(25).EmailAddress();
      RuleFor(c => c.Password).NotEmpty().NotNull().MinimumLength(6);
    }
  }
}
