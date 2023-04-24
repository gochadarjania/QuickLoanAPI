using QuickLoanAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Infrastructure.DbManage.Interfaces
{
  public interface ICustomerRepository
  {
    Task Add(LoanRequest loanRequest);
    Task Update(LoanRequest loanRequest);
    Task<LoanRequest> GetLoan(int id, int userId);
    Task<(List<LoanRequest>, int)> GetLoans(int userId, int page, int limit);
  }
}
