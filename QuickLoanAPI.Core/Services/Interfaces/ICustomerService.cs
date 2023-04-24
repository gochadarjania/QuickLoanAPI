using Azure;
using Microsoft.AspNetCore.Http;
using QuickLoanAPI.Application.Objects;
using QuickLoanAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Application.Services.Interfaces
{
  public interface ICustomerService
  {
    Task<string> Add(LoanRequestForm loanRequest, HttpRequest httpRequest);
    Task Update(LoanRequestForm loanRequest, HttpRequest httpRequest);
    Task<LoanRequestForm> GetLoan(int id, HttpRequest httpRequest);
    Task<(List<LoanRequestForm>, int)> GetLoans(HttpRequest httpRequest,int page,int limit);
  }
}
