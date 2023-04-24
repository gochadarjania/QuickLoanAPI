using FluentValidation;
using QuickLoanAPI.Application.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Application.Validators
{
  public class LoanValidator : AbstractValidator<LoanRequestForm>
  {
    public LoanValidator()
    {
      RuleFor(c => c.Period).NotEmpty().NotNull();
      RuleFor(c => c.CurrencyTypeId).NotEmpty().NotNull();
      RuleFor(c => c.Money).NotEmpty().NotNull();
      RuleFor(c => c.LoanTypeId).NotEmpty().NotNull();
    }
  }
}
