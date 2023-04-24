using Microsoft.EntityFrameworkCore;
using QuickLoanAPI.Domain;
using QuickLoanAPI.Domain.Helpers;
using QuickLoanAPI.Infrastructure.DbManage.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Infrastructure.DbManage
{
  public class AdminRepository : IAdminRepository
  {
    private LoanDbContext _dbContext;
    public AdminRepository(LoanDbContext dbContext)
    {
      _dbContext = dbContext;
    }
    public async Task Approve(int loanId)
    {
      var loan = await _dbContext.LoanRequests.
        FirstOrDefaultAsync(x => x.Id == loanId);
      if (loan == null)
      {
        throw new ClientException($"განაცხადი არ მოიძებნა");
      }
      loan.StatusId = Application.ProgressStatus.Approved;

      await _dbContext.SaveChangesAsync();
    }
    public async Task Delete(int loanId)
    {
      var loan = await _dbContext.LoanRequests.
        FirstOrDefaultAsync(x => x.Id == loanId);
      if (loan == null)
      {
        throw new ClientException($"განაცხადი არ მოიძებნა");
      }

      _dbContext.LoanRequests.Remove(loan);

      await _dbContext.SaveChangesAsync();
    }
    public async Task<(List<LoanRequest>, int)> GetLoans(int page, int limit)
    {
      int start = (page - 1) * limit;
      var loans = await (from loan in _dbContext.LoanRequests
                         orderby loan.Id descending
                         select loan)
                        .Skip(start)
                        .Take(limit)
                        .ToListAsync();

      int total = await _dbContext.LoanRequests.CountAsync();

      return (loans, total);
    }
    public async Task<LoanRequest> GetLoan(int loanId)
    {
      var loan = await _dbContext.LoanRequests.
        FirstOrDefaultAsync(x => x.Id == loanId);
      return loan;
    }

    public async Task Update(LoanRequest loan)
    {
      _dbContext.LoanRequests.Update(loan);
      await _dbContext.SaveChangesAsync();
    }
  }
}
