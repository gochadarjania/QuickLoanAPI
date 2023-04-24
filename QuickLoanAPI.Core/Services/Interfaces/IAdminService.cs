using Microsoft.AspNetCore.Http;
using QuickLoanAPI.Application.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Application.Services.Interfaces
{
  public interface IAdminService
  {
    Task Delete(int loanId);
    Task Approve(int loanId);
    Task<(List<LoanRequestForm>, int)> GetLoans(int page, int limit);
    Task<LoanRequestForm> GetLoan(int loanId);
    Task Update(LoanRequestForm loanModel);
  }
}
