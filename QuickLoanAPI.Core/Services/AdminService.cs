using Microsoft.AspNetCore.Http;
using QuickLoanAPI.Application.Helpers;
using QuickLoanAPI.Application.Objects;
using QuickLoanAPI.Application.Services.Interfaces;
using QuickLoanAPI.Infrastructure.DbManage;
using QuickLoanAPI.Infrastructure.DbManage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Application.Services
{
  public class AdminService : IAdminService
  {
    private readonly IAdminRepository _adminRepositpry;
    public AdminService(IAdminRepository loanRepositpry)
    {
      _adminRepositpry = loanRepositpry;
    }
    public async Task Approve(int loanId)
    {
      await _adminRepositpry.Approve(loanId);
    }
    public async Task Delete(int loanId)
    {
      await _adminRepositpry.Delete(loanId);
    }

    public async Task<LoanRequestForm> GetLoan(int loanId)
    {
      var loan = await _adminRepositpry.GetLoan(loanId);

      return loan.MapLoanToLoanForm();
    }

    public async Task<(List<LoanRequestForm>, int)> GetLoans(int page, int limit)
    {
      var loans = await _adminRepositpry.GetLoans(page, limit);

      var loanForms = loans.Item1.Select(loan => loan.MapLoanToLoanForm()).ToList();

      return (loanForms, loans.Item2);
    }

    public async Task Update(LoanRequestForm loanModel)
    {
      var loan = loanModel.MapLoanUpdateByAdmin();
      await _adminRepositpry.Update(loan);
    }
  }
}
