using QuickLoanAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Infrastructure.DbManage.Interfaces
{
  public interface IAdminRepository 
  {
    Task Approve(int loanId);
    Task Delete(int loanId);
    Task<(List<LoanRequest>, int)> GetLoans(int page, int limit);
    Task<LoanRequest> GetLoan(int loanId);
    Task Update(LoanRequest loan);
  }
}
