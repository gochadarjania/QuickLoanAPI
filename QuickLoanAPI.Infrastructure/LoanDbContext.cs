using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Infrastructure
{
  public class LoanDbContext : DbContext
  {
    public LoanDbContext(DbContextOptions<LoanDbContext> options) :
        base(options)
    {

    }
  }
}
