using FluentValidation;
using Microsoft.AspNetCore.Http;
using QuickLoanAPI.Application.Helpers;
using QuickLoanAPI.Application.Objects;
using QuickLoanAPI.Application.Services.Interfaces;
using QuickLoanAPI.Application.Validators;
using QuickLoanAPI.Domain;
using QuickLoanAPI.Domain.Helpers;
using QuickLoanAPI.Infrastructure.DbManage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Application.Services
{
  public class CustomerService : ICustomerService
  {
    private readonly ICustomerRepository _customerRepositpry;
    public CustomerService(ICustomerRepository customerRepositpry)
    {
      _customerRepositpry = customerRepositpry;
    }
    public async Task<string> Add(LoanRequestForm loanRequest, HttpRequest httpRequest)
    {
      var loanValidator = new LoanValidator();
      var result = loanValidator.Validate(loanRequest);
      if (!result.IsValid)
      {
        throw new ClientException(string.Join("\n", result.Errors));
      }

      var loan = loanRequest.MapLoan(httpRequest);

      await _customerRepositpry.Add(loan);

      return "განაცხადი დაემატა წარმატებით.";
    }
 
    public async Task Update(LoanRequestForm loanRequest, HttpRequest httpRequest)
    {
      var loanValidator = new LoanValidator();
      var result = loanValidator.Validate(loanRequest);
      if (!result.IsValid)
      {
        throw new ClientException(string.Join("\n", result.Errors));
      }
      if (loanRequest.StatusId == ProgressStatus.Approved || loanRequest.StatusId == ProgressStatus.Rejected)
      {
        throw new ClientException($"განაცხადის რედაქტირება სამწუხაროდ შეუძლებელია.");
      }
      var loan = loanRequest.MapLoanUpdate(httpRequest);
      await _customerRepositpry.Update(loan);
    }
    public async Task<LoanRequestForm> GetLoan(int id, HttpRequest httpRequest)
    {
      var userId = httpRequest.GetUserIdbyJWT();
      var loan = await _customerRepositpry.GetLoan(id, userId);

      return loan.MapLoanToLoanForm();
    }
    public async Task<(List<LoanRequestForm>, int)> GetLoans(HttpRequest httpRequest, int page, int limit)
    {
      var userId = httpRequest.GetUserIdbyJWT();
      var loans = await _customerRepositpry.GetLoans(userId, page, limit);

      var loanForms = loans.Item1.Select(loan => loan.MapLoanToLoanForm()).ToList();

      return (loanForms, loans.Item2);
    }
  }
}
