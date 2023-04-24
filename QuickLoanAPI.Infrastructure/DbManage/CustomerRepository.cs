using Microsoft.EntityFrameworkCore;
using QuickLoanAPI.Domain;
using QuickLoanAPI.Domain.Helpers;
using QuickLoanAPI.Infrastructure.DbManage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace QuickLoanAPI.Infrastructure.DbManage
{
  public class CustomerRepository : ICustomerRepository
  {
    private LoanDbContext _dbContext;
    public CustomerRepository(LoanDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<LoanRequest> GetLoan(int id, int userId)
    {
      var loan = await _dbContext.LoanRequests.
        FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
      return loan;
    }

    public async Task<(List<LoanRequest>, int)> GetLoans(int userId, int page, int limit)
    {
      int start = (page - 1) * limit;
      var loans = await (from loan in _dbContext.LoanRequests
                         where loan.UserId == userId
                         orderby loan.Id descending
                         select loan)
                        .Skip(start)
                        .Take(limit)
                        .ToListAsync();

      int total = await _dbContext.LoanRequests.CountAsync(loan => loan.UserId == userId);


      return (loans, total);
    }
    public async Task Add(LoanRequest loanRequest)
    {
      await _dbContext.AddAsync(loanRequest);
      await _dbContext.SaveChangesAsync();
    }


    public async Task Update(LoanRequest loanRequest)
    {
      var loan = await _dbContext.LoanRequests.FirstOrDefaultAsync(x => x.Id == loanRequest.Id);
      if (loan == null)
      {
        throw new ClientException($"განაცხადი არ მოიძებნა");
      }
      if (loan.UserId != loanRequest.UserId)
      {
        throw new ClientException($"რედაქტირება შეუძლებელია");
      }

      loan.LoanTypeId = loanRequest.LoanTypeId;
      loan.Period = loanRequest.Period;
      loan.Money = loanRequest.Money;
      loan.CurrencyTypeId = loanRequest.CurrencyTypeId;
      loan.UpdatedAt = DateTime.Now;

      await _dbContext.SaveChangesAsync();
    }

  }
}
